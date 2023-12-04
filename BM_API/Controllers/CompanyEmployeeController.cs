using AutoMapper;
using BM_API.Models;
using BM_API.Repositories;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BM_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyEmployeeController:ControllerBase
    {
        private readonly ICompanyEmployeeRepository? _companyEmployeeRepository;
        private readonly IEmployeeRepository? _employeeRepository;
        private readonly ICompanyRepository? _companyRepository;
        private readonly IMapper? _mapper;

        public CompanyEmployeeController(ICompanyEmployeeRepository? companyEmployeeRepository, IEmployeeRepository? employeeRepository, ICompanyRepository? companyRepository, IMapper? mapper)
        {
            _companyEmployeeRepository = companyEmployeeRepository;
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet("get-employees-by-company/{companyId}")]
        public async Task<IActionResult> GetEmployeesByCompany(Guid companyId)
        {
            try
            {
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("Company doesn't exist.");
                }

                ICollection<CompanyEmployee> companyEmployees = await _companyEmployeeRepository.GetEmployeesByCompanyAsync(companyId);
                List<Employee> employees = new List<Employee>();

                foreach (CompanyEmployee ce in companyEmployees)
                {
                    ce.Employee = await _employeeRepository.GetEmployeeByIdAsync(ce.EmployeeId);
                    employees.Add(ce.Employee);
                }

                if (employees == null || employees.Count == 0)
                {
                    return NotFound("Employees not found");
                }

                return Ok(employees);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("add-company-employee")]
        public async Task<IActionResult> AddCompanyEmployee([FromBody] CompanyEmployee companyEmployee)
        {
            try
            {
                if (companyEmployee == null)
                {
                    return BadRequest("Company employee is null.");
                }
                Employee? employee = await _employeeRepository.GetEmployeeByIdAsync(companyEmployee.EmployeeId);
                if (employee == null)
                {
                    return BadRequest("Employee not found.");
                }
                Company company = await _companyRepository.GetCompanyByIdAsync(companyEmployee.CompanyId);
                if (company == null)
                {
                    return BadRequest("Company not found.");
                }
                var foundCompanyEmployee = await _companyEmployeeRepository.GetCompanyEmployeeByEmployeeId(companyEmployee.EmployeeId);
                if (foundCompanyEmployee != null)
                {
                    return BadRequest("Employee already exists in this company.");
                }
                companyEmployee.Employee = employee;
                companyEmployee.Company = company;
                _companyEmployeeRepository.Add(companyEmployee);
                if (await _companyEmployeeRepository.SaveChangesAsync())
                {

                    return Ok(companyEmployee);
                }

                return BadRequest("Db failure.");
            }
            catch (Exception)
            {
                return BadRequest("Db failure");
            }
        }

        [HttpDelete("delete-company-employee/{employeeId}")]
        public async Task<IActionResult> DeleteCompanyEmployee([FromRoute] Guid employeeId)
        {
            try
            {
                if(employeeId==null)
                {
                    return BadRequest("Invalid employee");
                }
                CompanyEmployee companyEmployee = await _companyEmployeeRepository.GetCompanyEmployeeByEmployeeId(employeeId);
                if (companyEmployee == null)
                {
                    return NotFound("Employee not found in company.");
                }
                _companyEmployeeRepository.Delete(companyEmployee);
                if(await _companyEmployeeRepository.SaveChangesAsync())
                { 
                    return Ok(companyEmployee); 
                }
                return BadRequest("Db fail.");

            }
            catch(Exception)
            {
                return BadRequest("Db fail.");
            }
        }
    }
}
