using BM_API.DTOs.Account;
using BM_API.Models;
using Microsoft.AspNetCore.Identity;

namespace BM_API.Repositories.RepositoryInterfaces
{
    public interface IAccountRepository:IRepository
    {
        UserDto CreateApplicationUserDto(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<SignInResult> CheckPasswordAsync(User user, string password, bool failure);
        Task<IdentityResult> CreateUserAsync(User user,string password);
        Task<IdentityResult> ConfirmUserEmailAsync(User user, string decodedToken);
        Task<IdentityResult> ResetUserPasswordAsync(User user, string decodedToken, string newPassword);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> SendConfirmEmailAsync(User user);
        Task<bool> SendForgotUsernameOrPasswordEmail(User user);
    }
}
