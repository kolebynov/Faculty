using System.Threading.Tasks;
using Faculty.EFCore.Domain;

namespace Faculty.EFCore.Services.Users
{
    public interface IUserService
    {
        Task<UserResult> LoginAsync(LoginData loginData);
        Task LogoutAsync();
        Task<UserResult> RegisterNewUserAsync(RegisterUserData registerUserData);
        Task<User> GetCurrentUser();
    }
}