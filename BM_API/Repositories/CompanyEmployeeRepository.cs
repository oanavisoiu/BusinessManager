using BM_API.Data;
using BM_API.DTOs.Employee;
using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Repositories
{
    public class CompanyEmployeeRepository : Repository, ICompanyEmployeeRepository
    {
        private readonly BMDbContext _bmDbContext;
        private readonly IEmployeeRepository _employeeRepository;

        public CompanyEmployeeRepository(BMDbContext bmDbContext, IEmployeeRepository employeeRepository) : base(bmDbContext)
        {
            _bmDbContext = bmDbContext;
            _employeeRepository = employeeRepository;
        }
        public async Task<CompanyEmployee> GetCompanyEmployeeByEmployeeId(Guid employeeId)
        {
            CompanyEmployee? companyEmployee = await _bmDbContext.CompanyEmployees.FirstOrDefaultAsync(x => x.EmployeeId.Equals(employeeId));
            return companyEmployee;
        }
        public async Task<ICollection<CompanyEmployee>> GetEmployeesByCompanyAsync(Guid companyId)
        {
            var employees = await _bmDbContext.CompanyEmployees.Where(x => x.CompanyId.Equals(companyId)).ToListAsync();

            return employees;
        }
        public async Task<decimal> GetSumOfSalariesAsync(Guid companyId)
        {
            decimal sum = 0;
            var companyEmployees = await GetEmployeesByCompanyAsync(companyId);

            foreach (var companyEmployee in companyEmployees)
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(companyEmployee.EmployeeId);
                sum += employee.Salary;
            }

            return sum;
        }

        public async Task<ICollection<Employee>> GetEmployeesBirthdaysForAMonthAsync(Guid companyId)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddMonths(1);
            var companyEmployees=await GetEmployeesByCompanyAsync(companyId);
            var employees = companyEmployees
                .Join(
                _bmDbContext.Employees,
                ce => ce.EmployeeId,
                e => e.Id,
                (ce,e)=>e)
                .Where(e=>(e.BirthDate.Day>=startDate.Day&&e.BirthDate.Month==startDate.Month)||
                (e.BirthDate.Day<=endDate.Day&&e.BirthDate.Month==endDate.Month)||
                (e.BirthDate.Month>startDate.Month&&e.BirthDate.Month<endDate.Month));
            return employees.ToList();
        }
    
    }
}
