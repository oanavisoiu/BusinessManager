using AutoMapper;
using BM_API.DTOs.EmployeeUpdateDto;
using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BM_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository? _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository? employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        
        [HttpGet("get-employee/{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("The id is empty");
                }

                Employee employee = await _employeeRepository.GetEmployeeByIdAsync(id);

                if (employee == null)
                {
                    return NotFound("Employee not found");
                }

                return Ok(employee);
            }
            catch (Exception)
            {
                return BadRequest("Db failure");
            }
        }

        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeUpdateDTO employeeRequest)
        {
            try
            {
                Employee employee = _mapper.Map<Employee>(employeeRequest);
                employee.Id = Guid.NewGuid();
                _employeeRepository.Add(employee);
                if (await _employeeRepository.SaveChangesAsync())
                    return Ok(employee);
                return BadRequest("Db failure");
            }
            catch (Exception)
            {
                return BadRequest("Db failure");
            }
        }

        [HttpPut("update-employee/{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id,[FromBody] EmployeeUpdateDTO updatedEmployee)
        {
            try
            {
                if(id == Guid.Empty)
                { return BadRequest("Id is empty."); }
                Employee employee = _mapper.Map<Employee>(updatedEmployee);
                employee.Id=id;
                _employeeRepository.Update(employee);
                if(await _employeeRepository.SaveChangesAsync())
                    return Ok(employee);
                return BadRequest("Db failure");
            }
            catch(Exception)
            {
                return BadRequest("Db failure");
            }
        }
        [HttpDelete("Delete-employee/{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                { return BadRequest("Id is empty."); }
                Employee employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if(employee==null)
                {
                    return NotFound("Employee not found.");
                }
                _employeeRepository.Delete(employee);
                if (await _employeeRepository.SaveChangesAsync())
                    return Ok(employee);
                return BadRequest("Db failure");
            }
            catch (Exception)
            {
                return BadRequest("Db failure");
            }
        }

        
    }

}

