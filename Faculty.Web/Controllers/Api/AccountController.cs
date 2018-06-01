using System;
using System.Threading.Tasks;
using Faculty.EFCore.Services.Users;
using Faculty.Web.ApiResults;
using Faculty.Web.Services.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Faculty.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IApiHelper _apiHelper;

        public AccountController(IUserService userService, IApiHelper apiHelper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _apiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginData loginData)
        {
            if (ModelState.IsValid)
            {
                UserResult loginResult = await _userService.LoginAsync(loginData);
                return Json(loginResult);
            }

            return Json(_apiHelper.GetErrorResultFromModelState(ModelState));
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegisterUserData registerData)
        {
            if (ModelState.IsValid)
            {
                UserResult registerResult = await _userService.RegisterNewUserAsync(registerData);
                return Json(registerResult);
            }

            return Json(_apiHelper.GetErrorResultFromModelState(ModelState));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return Json(new ApiResult {Success = true});
        }

        public async Task<IActionResult> GetCurrentUser() => Json(await _userService.GetCurrentUser());
    }
}