using BM_API.Data;
using BM_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly BMDbContext _bmDbContext;
        public EmployeesController(BMDbContext bmDbContext)
        {
            _bmDbContext = bmDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _bmDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _bmDbContext.Employees.AddAsync(employeeRequest);
            await _bmDbContext.SaveChangesAsync();
            return Ok(employeeRequest);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _bmDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
                return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployee)
        {
            var employee = await _bmDbContext.Employees.FindAsync(updateEmployee.Id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                employee.Name = updateEmployee.Name;
                employee.Email = updateEmployee.Email;
                employee.Phone = updateEmployee.Phone;
                employee.Salary = updateEmployee.Salary;
                employee.Department = updateEmployee.Department;
                await _bmDbContext.SaveChangesAsync();
                return Ok(employee);
            }
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _bmDbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }
            _bmDbContext.Employees.Remove(employee);
            _bmDbContext.SaveChanges();
            return Ok();

        }

    }

}

