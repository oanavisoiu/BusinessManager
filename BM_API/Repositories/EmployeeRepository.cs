using BM_API.Data;
using BM_API.DTOs.EmployeeUpdateDto;
using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Repositories
{
    public class EmployeeRepository:Repository, IEmployeeRepository
    {
        private readonly BMDbContext _bmDbContext;

        public EmployeeRepository(BMDbContext bmDbContext) : base(bmDbContext)
        {
            _bmDbContext = bmDbContext;
        }
        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            Employee? employee = await _bmDbContext.Employees.FirstAsync(x => x.Id.Equals(id));
            return employee;
        }
        
    }
}
