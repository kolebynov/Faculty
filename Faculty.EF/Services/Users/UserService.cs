using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Faculty.EFCore.Domain;
using Faculty.EFCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Faculty.EFCore.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<UserResult> LoginAsync(LoginData loginData)
        {
            loginData.CheckArgumentNull(nameof(loginData));
            User user = await _userManager.FindByNameAsync(loginData.UserName);
            UserResult loginResult = new UserResult { Success = false };
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                loginResult = FromSignInResult(await _signInManager.PasswordSignInAsync(user, loginData.Password, false, false));
            }

            return loginResult;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _httpContextAccessor.HttpContext.Session.Clear();
        }

        public async Task<UserResult> RegisterNewUserAsync(RegisterUserData registerUserData)
        {
            registerUserData.CheckArgumentNull(nameof(registerUserData));
            User newUser = CreateNewUser(registerUserData);

            return FromIdentityResult(await _userManager.CreateAsync(newUser, registerUserData.Password));
        }

        public async Task<User> GetCurrentUser() => await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        private UserResult FromSignInResult(SignInResult signInResult) => new UserResult
        {
            Success = signInResult.Succeeded
        };

        private UserResult FromIdentityResult(IdentityResult identityResult) => new UserResult
        {
            Success = identityResult.Succeeded,
            Errors = identityResult.Errors.Select(e => new UserError
            {
                Code = e.Code,
                Message = e.Description
            }),
        };

        private User CreateNewUser(RegisterUserData registerUserData) => new User
        {
            Email = registerUserData.Email,
            UserName = registerUserData.UserName,
            FirstName = registerUserData.FirstName,
            Name = registerUserData.Name
        };
    }
}
