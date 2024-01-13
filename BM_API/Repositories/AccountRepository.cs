using BM_API.Data;
using BM_API.DTOs.Account;
using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using BM_API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BM_API.Repositories
{
    public class AccountRepository : Repository, IAccountRepository 
    {
        private readonly BMDbContext _bmDbContext;
        private readonly UserManager<User> _userManager;
        private readonly JWTService _jwtService;
        private readonly EmailService _emailService;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountRepository(BMDbContext bMDbContext, UserManager<User> userManager, JWTService jwtService, EmailService emailService, SignInManager<User> signInManager, IConfiguration configuration) : base(bMDbContext)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _emailService = emailService;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public UserDto CreateApplicationUserDto(User user)
        {
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                JWT = _jwtService.CreateJWT(user)
            };
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<SignInResult> CheckPasswordAsync(User user, string password, bool failure)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, failure);
        }
        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(u => u.Email == email.ToLower());
        }
        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<IdentityResult> ConfirmUserEmailAsync(User user, string decodedToken)
        {
            return await _userManager.ConfirmEmailAsync(user, decodedToken);
        }
        public async Task<IdentityResult> ResetUserPasswordAsync(User user, string decodedToken, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);
        }
        public async Task<bool> SendConfirmEmailAsync(User user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{_configuration["JWT:ClientUrl"]}/{_configuration["Email:ConfirmationEmailPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Hello: {user.FirstName} {user.LastName}</p>" +
                "<p>Please confirm your email by clicking on the following link</p>" +
                $"<p><a href=\"{url}\">Click here</a></p>" +
                "<p>Thank you!</p>" +
                $"<br>{_configuration["Email:ApplicationName"]}";
            var emailSend = new EmailSendDto(user.Email, "Confirm your email", body);
            return await _emailService.SendEmailAsync(emailSend);
        }
        public async Task<bool> SendForgotUsernameOrPasswordEmail(User user)
        {

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{_configuration["JWT:ClientUrl"]}/{_configuration["Email:ResetPasswordPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Hello: {user.FirstName} {user.LastName}</p>" +
                $"<p>Username: {user.UserName}</p>" +
                "<p>In order to reset your password, please click on the following link.</p>" +
                $"<p><a href=\"{url}\">Click here</a></p>" +
                "<p>Thank you!</p>" +
                $"<br>{_configuration["Email:ApplicationName"]}";
            var emailSend = new EmailSendDto(user.Email, "Forgot username or password", body);
            return await _emailService.SendEmailAsync(emailSend);
        }

    }
}
