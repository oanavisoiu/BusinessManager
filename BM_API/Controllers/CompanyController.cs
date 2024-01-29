using BM_API.DTOs.Company;
using BM_API.DTOs.ToDo;
using BM_API.Models;
using BM_API.Repositories;
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
        private readonly ICompanyEmployeeRepository _companyEmployeeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanySupplierRepository _companySupplierRepository;
        private readonly ISupplierRepository _supplierRepository;
        public CompanyController(ICompanyRepository companyRepository, IAccountRepository accountRepository, ICompanyEmployeeRepository companyEmployeeRepository, IEmployeeRepository employeeRepository, ICompanySupplierRepository companySupplierRepository, ISupplierRepository supplierRepository)
        {
            _companyRepository = companyRepository;
            _accountRepository = accountRepository;
            _companyEmployeeRepository = companyEmployeeRepository;
            _employeeRepository = employeeRepository;
            _companySupplierRepository = companySupplierRepository;
            _supplierRepository = supplierRepository;
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
        [HttpDelete("delete-company")]
        public async Task<IActionResult> DeleteCompany()
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
                ICollection<CompanyEmployee> companyEmployees = await _companyEmployeeRepository.GetEmployeesByCompanyAsync(company.Id);
                if(companyEmployees.Count>0)
                {
                    foreach(CompanyEmployee ce in companyEmployees)
                    {
                        Employee emp = await _employeeRepository.GetEmployeeByIdAsync(ce.EmployeeId);
                        _employeeRepository.Delete(emp);
                    }
                }
                ICollection<CompanySupplier> companySuppliers = await _companySupplierRepository.GetCompanySuppliersByCompanyIdAsync(company.Id);
                if (companySuppliers.Count > 0)
                {
                    foreach (CompanySupplier cs in companySuppliers)
                    {
                        Supplier sup = await _supplierRepository.GetSupplierByIdAsync(cs.SupplierId);
                        _supplierRepository.Delete(sup);
                    }
                }
                _companyRepository.Delete(company);
                if(await _companyRepository.SaveChangesAsync())
                {
                    return Ok(company);
                }
                return BadRequest("Db fail.");
            }
            catch (Exception)
            {
                return BadRequest("Db fail.");
            }
        }
        [HttpPut("update-company")]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyDTO companyAdd)
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
                company.Name=companyAdd.Name;
                company.Address=companyAdd.Address;
                company.PhoneNumber=companyAdd.PhoneNumber;
                _companyRepository.Update(company);
                if (await _companyRepository.SaveChangesAsync())
                {
                    return Ok(company);
                }
                return BadRequest("Db fail");
            }
            catch (Exception)
            {
                return BadRequest("Db fail");
            }
        }

    }
}
