using BM_API.Data;
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
        public async Task<long> GetSumOfSalariesAsync(Guid companyId)
        {
            long sum = 0;
            var companyEmployees = await GetEmployeesByCompanyAsync(companyId);

            foreach (var companyEmployee in companyEmployees)
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(companyEmployee.EmployeeId);
                sum += employee.Salary;
            }

            return sum;
        }
        
    }
}
