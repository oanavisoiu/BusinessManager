using BM_API.DTOs.Account;
using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;

namespace BM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<UserDto>> RefreshUserToken()
        {
            User user = await _accountRepository.GetUserByEmailAsync(User.FindFirst(ClaimTypes.Email)?.Value);
            return _accountRepository.CreateApplicationUserDto(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            try
            {
                User user = await _accountRepository.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    return Unauthorized("Invalid email or password");
                }
                if (user.EmailConfirmed == false)
                {
                    return Unauthorized("Please confirm your email.");
                }
                var result = await _accountRepository.CheckPasswordAsync(user, model.Password, false);
                if (!result.Succeeded)
                {
                    return Unauthorized("Invalid email or password");
                }
                return _accountRepository.CreateApplicationUserDto(user);
            }
            catch (Exception)
            {
                return BadRequest("Failed to login.");
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (await _accountRepository.CheckEmailExistsAsync(model.Email))
            {
                return BadRequest($"An existing account is using {model.Email}. Please try with another email.");
            }
            var userToAdd = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email.ToLower(),
                Email = model.Email.ToLower()
            };
            var result = await _accountRepository.CreateUserAsync(userToAdd, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest("Password should have at least one uppercase, one lowercase and one digit.");
            }

            try
            {
                if (await _accountRepository.SendConfirmEmailAsync(userToAdd))
                {
                    return Ok(new JsonResult(new { title = "Created account", message = "Your account has successfully been created! Please confirm your email adress." }));
                }
                return BadRequest("Failed to send email.Try contact the admin.");
            }
            catch (Exception)
            {
                return BadRequest("Failed to send email.Try contact the admin.");
            }

        }

        [HttpPut("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto model)
        {
            var user = await _accountRepository.GetUserByEmailAsync(model.Email);
            if (user == null) return Unauthorized("This email address ahs not been registered yet.");

            if (user.EmailConfirmed == true)
            {
                return BadRequest("your email was confirmed before. Please login to your account.");
            }
            try
            {
                var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.Token);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

                var result = await _accountRepository.ConfirmUserEmailAsync(user, decodedToken);
                if (result.Succeeded)
                {
                    return Ok(new JsonResult(new { title = "Email confirmed", message = "Your email address is confirmed. You can login now." }));
                }
                return BadRequest("Invalid token. Please try again.");
            }
            catch (Exception)
            {
                return BadRequest("Invalid token. Please try again.");
            }
        }
        [HttpPost("resend-email-confirmation-link/{email}")]
        public async Task<IActionResult> ResendEmailConfirmationLink(string email)
        {
            if (string.IsNullOrEmpty(email)) return BadRequest("Invalid email.");
            var user = await _accountRepository.GetUserByEmailAsync(email);

            if (user == null) return Unauthorized("This email address has not been registered yet");
            if (user.EmailConfirmed == true) return BadRequest("Your email address was confirmed before. Please login to your account.");
            try
            {
                if (await _accountRepository.SendConfirmEmailAsync(user)) return Ok(new JsonResult(new { title = "Email sent", message = "Check your email to confirm your email." }));
                return BadRequest("Fail to send email. Please contact admin.");
            }
            catch (Exception)
            {
                return BadRequest("Fail to send email. Please contact admin.");
            }
        }
        [HttpPost("forgot-username-or-password/{email}")]
        public async Task<IActionResult> ForgotUsernameOrPassword(string email)
        {
            if (string.IsNullOrEmpty(email)) return BadRequest("Invalid email.");
            var user = await _accountRepository.GetUserByEmailAsync(email);
            if (user == null) return Unauthorized("This email address has not been registered.");
            if (user.EmailConfirmed == false) return BadRequest("Please confirm your email address first.");
            try
            {
                if (await _accountRepository.SendForgotUsernameOrPasswordEmail(user))
                {
                    return Ok(new JsonResult(new { title = "Forgot username or password email sent", message = "Please check your email." }));
                }
                return BadRequest("Failed to send email. Please contact admin");
            }
            catch (Exception)
            {
                return BadRequest("Failed to send email. Please contact admin");
            }
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await _accountRepository.GetUserByEmailAsync(model.Email);
            if (user == null) return Unauthorized("This email address has not been registered yet.");
            if (user.EmailConfirmed == false) return BadRequest("Please confirm your email address.");
            try
            {
                var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.Token);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

                var result = await _accountRepository.ResetUserPasswordAsync(user, decodedToken, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok(new JsonResult(new { title = "Password reset success", message = "Your password has been reset" }));
                }
                return BadRequest("Invalid token. Please try again.");
            }
            catch (Exception)
            {
                return BadRequest("Invalid token. Please try again.");
            }
        }
        
    }
}
