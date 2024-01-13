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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAccountRepository _accountRepository;
        public CompanyController(ICompanyRepository companyRepository, IAccountRepository accountRepository)
        {
            _companyRepository = companyRepository;
            _accountRepository = accountRepository;
        }
        [HttpPost("create-company")]
        public async Task<ActionResult> CreateCompany([FromBody] Company company)
        {
            try
            {
                company.Id = Guid.NewGuid();
                User user = await _accountRepository.GetUserByEmailAsync(User.FindFirst(ClaimTypes.Email)?.Value);
                if (user == null)
                {
                    return NotFound("No user found.");
                }
                company.User= user;
                _companyRepository.Add(company);
                if(await _companyRepository.SaveChangesAsync())
                    return Ok(company);
                return BadRequest("Db failure.");
            }
            catch (Exception)
            {
                return BadRequest("Db failure.");
            }
        }

        [HttpGet("get-company")]
        public async Task<IActionResult> GetCompany()
        {
            try
            {
                User loggedUser = await _accountRepository.GetUserByEmailAsync(User.FindFirst(ClaimTypes.Email)?.Value);
                if (loggedUser == null)
                {
                    return BadRequest("User is not logged.");
                }
                Company company = await _companyRepository.GetCompanyAsync(loggedUser);
                if (company == null)
                {
                    return NotFound("No company found.");
                }
                return Ok(company);
            }
            catch(Exception)
            {
                return BadRequest("Db fail.");
            }
        }
        
    }
}
