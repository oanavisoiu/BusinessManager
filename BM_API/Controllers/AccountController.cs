using BM_API.DTOs.Account;
using BM_API.Models;
using BM_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace BM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTService jwtService;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly EmailService emailService;
        private readonly IConfiguration configuration;

        public AccountController(JWTService jwtService, SignInManager<User> signInManager, UserManager<User> userManager, EmailService emailService, IConfiguration configuration)
        {
            this.jwtService = jwtService;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.emailService = emailService;
            this.configuration = configuration;
        }

        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<UserDto>> RefreshUserToken()
        {
            var user = await userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email)?.Value);
            return CreateApplicationUserDto(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }
            if (user.EmailConfirmed == false)
            {
                return Unauthorized("Please confirm your email.");
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid email or password");
            }
            return CreateApplicationUserDto(user);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (await CheckEmailExistsAsync(model.Email))
            {
                return BadRequest($"An existing account is using {model.Email}. Please try with another email.");
            }
            var userToAdd = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email.ToLower(),
                Email = model.Email.ToLower(),
            };
            var result = await userManager.CreateAsync(userToAdd, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest("Password should have at least one uppercase, one lowercase and one digit.");
            }

            try
            {
                if (await SendConfirmEmailAsync(userToAdd))
                {
                    return Ok(new JsonResult(new { title = "Email sent", message = "Check your email to confirm your email." }));
                }
                return BadRequest("Failed to send email.Try contact the admin.");
            } catch (Exception) {
                return BadRequest("Failed to send email.Try contact the admin.");
            }

        }

        [HttpPut("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized("This email address ahs not been registered yet.");

            if(user.EmailConfirmed==true)
            {
                return BadRequest("your email was confirmed before. Please login to your account.");
            }
            try
            {
                var decodedTokenBytes=WebEncoders.Base64UrlDecode(model.Token);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

                var result = await userManager.ConfirmEmailAsync(user,decodedToken);
                if(result.Succeeded)
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
            var user = await userManager.FindByEmailAsync (email);

            if (user == null) return Unauthorized("This email address has not been registered yet");
            if (user.EmailConfirmed == true) return BadRequest("Your email address was confirmed before. Please login to your account.");
            try
            {
                if(await SendConfirmEmailAsync(user)) return Ok(new JsonResult(new { title = "Email sent", message = "Check your email to confirm your email." }));
                return BadRequest("Fail to send email. Please contact admin.");
            }
            catch(Exception)
            {
                return BadRequest("Fail to send email. Please contact admin.");
            }
        }
        [HttpPost("forgot-username-or-password/{email}")]
        public async Task<IActionResult> ForgotUsernameOrPassword(string email)
        {
            if (string.IsNullOrEmpty(email)) return BadRequest("Invalid email.");
            var user= await userManager.FindByEmailAsync (email);
            if (user == null) return Unauthorized("This email address has not been registered.");

            //if (user.EmailConfirmed == true) return BadRequest("Your email address was confirmed before. Please login to your account.");
            if (user.EmailConfirmed == false) return BadRequest("Please confirm your email address first.");
            try
            {
                if(await SendForgotUsernameOrPasswordEmail(user))
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
            var user = await userManager.FindByEmailAsync (model.Email);
            if (user == null) return Unauthorized("This email address has not been registered yet.");
            if (user.EmailConfirmed == false) return BadRequest("Please confirm your email address.");
            try
            {
                var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.Token);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

                var result = await userManager.ResetPasswordAsync(user, decodedToken,model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok(new JsonResult(new { title = "Password reset success", message = "Your password has been reset" }));
                }
                return BadRequest("Invalid token. Please try again.");
            }
            catch(Exception) {
                return BadRequest("Invalid token. Please try again.");
            }
        }


        #region Private Helper Methods
        private UserDto CreateApplicationUserDto(User user)
        {
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                JWT = jwtService.CreateJWT(user)
            };
        }
        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await userManager.Users.AnyAsync(u => u.Email == email.ToLower());
        }

        private async Task<bool> SendConfirmEmailAsync(User user)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{configuration["JWT:ClientUrl"]}/{configuration["Email:ConfirmationEmailPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Hello: {user.FirstName} {user.LastName}</p>" +
                "<p>Please confirm your email by clicking on the following link</p>" +
                $"<p><a href=\"{url}\">Click here</a></p>" +
                "<p>Thank you!</p>" +
                $"<br>{configuration["Email:ApplicationName"]}";
            var emailSend = new EmailSendDto(user.Email, "Confirm your email", body);
            return await emailService.SendEmailAsync(emailSend);
        }

        private async Task<bool> SendForgotUsernameOrPasswordEmail(User user)
        {

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{configuration["JWT:ClientUrl"]}/{configuration["Email:ResetPasswordPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Hello: {user.FirstName} {user.LastName}</p>" +
                $"<p>Username: {user.UserName}</p>" +
                "<p>In order to reset your password, please click on the following link.</p>"+
                $"<p><a href=\"{url}\">Click here</a></p>" +
                "<p>Thank you!</p>" +
                $"<br>{configuration["Email:ApplicationName"]}";
            var emailSend = new EmailSendDto(user.Email, "Forgot username or password", body);
            return await emailService.SendEmailAsync(emailSend);
        }
        #endregion
    }
}
