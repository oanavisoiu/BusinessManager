using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BM_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAccountRepository _accountRepository;
        public CompanyController(ICompanyRepository companyRepository, IAccountRepository accountRepository)
        {
            _companyRepository = companyRepository;
            _accountRepository = accountRepository;
        }
        [HttpPost("create-company")]
        public async Task<ActionResult> CreateCompany([FromBody] Company companyAdd)
        {
            try
            {
                User user = await _accountRepository.GetUserByEmailAsync(User.FindFirst(ClaimTypes.Email)?.Value);
                if (user == null)
                {
                    return NotFound("No user logged.");
                }
                Company company = new Company
                {
                    Name = companyAdd.Name,
                    Address = companyAdd.Address,
                    PhoneNumber = companyAdd.PhoneNumber,
                    Id = Guid.NewGuid(),
                    UserId = user.Id
                };
                _companyRepository.Add(company);
                await _companyRepository.SaveChangesAsync();
                return Ok(company);
            }
            catch (Exception)
            {
                return BadRequest("Failure?");
            }
        }

        [HttpGet("get-company")]
        public async Task<ActionResult<Company>> GetCompany()
        {
            try
            {
                User loggedUser = await _accountRepository.GetUserByEmailAsync(User.FindFirst(ClaimTypes.Email)?.Value);
                if (loggedUser == null)
                {
                    return BadRequest("User is not logged.");
                }
                Company company = await _companyRepository.GetCompanyByUserIdAsync(loggedUser.Id);
                if (company == null)
                {
                    return NotFound("No company found.");
                }
                return Ok(company);
            }
            catch(Exception e)
            {
                return BadRequest("Failed.");
            }
        }
        
    }
}
