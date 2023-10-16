using BM_API.DTOs.Account;
using BM_API.Models;
using BM_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTService jwtService;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountController(JWTService jwtService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.jwtService = jwtService;
            this.signInManager = signInManager;
            this.userManager = userManager;
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
                return Unauthorized("Verify your email");
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
                EmailConfirmed = true
            };
            var result =await userManager.CreateAsync(userToAdd, model.Password);

            if (!result.Succeeded) 
            { 
            return BadRequest("Password should have at least one uppercase, one lowercase and one digit.");
            }

            return Ok(new JsonResult(new{ title="Account created", message= "Your account has been created" }));
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
        #endregion
    }
}
