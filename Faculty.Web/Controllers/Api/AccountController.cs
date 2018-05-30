using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.EFCore.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faculty.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginData loginData)
        {
            if (ModelState.IsValid)
            {
                UserResult loginResult = await _userService.LoginAsync(loginData);
                if (loginResult.Success)
                {
                    return Redirect(returnUrl ?? "/");
                }

                ModelState.AddModelError("Email", "Неверный логин или пароль");
            }


        }

        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterUserData registerData)
        {
            if (ModelState.IsValid)
            {
                UserResult registerResult = await _userService.RegisterNewUserAsync(registerData);
                if (registerResult.Success)
                {
                    return RedirectToAction(nameof(Login));
                }

                foreach (UserError error in registerResult.Errors)
                    ModelState.AddModelError("", error.Message);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}