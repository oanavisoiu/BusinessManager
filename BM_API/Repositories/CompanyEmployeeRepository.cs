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
        public async Task<ICollection<Employee>> GetEmployeesWithEndDateAfterDateAsync(Guid companyId, DateTime date)
        {
            var companyEmployees = await GetEmployeesByCompanyAsync(companyId);
            var employees = companyEmployees
                .Join(
                _bmDbContext.Employees,
                ce => ce.EmployeeId,
                e => e.Id,
                (ce, e) => e)
                .Where(e => e.EndDate==null || e.EndDate>=date);
            return employees.ToList();
        }
        public async Task<double> GetSumOfSalariesAsync(Guid companyId, DateTime date)
        {
            double sum = 0;
            var employees = await GetEmployeesWithEndDateAfterDateAsync(companyId,date);

            foreach (var employee in employees)
            {
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
                .Where(e=>((e.BirthDate.Day>=startDate.Day&&e.BirthDate.Month==startDate.Month)||
                (e.BirthDate.Day<=endDate.Day&&e.BirthDate.Month==endDate.Month)||
                (e.BirthDate.Month>startDate.Month&&e.BirthDate.Month<endDate.Month))&&
                (e.EndDate>startDate)&&
                (
                    ((e.BirthDate.Month==e.EndDate.GetValueOrDefault().Month)&&
                    (e.BirthDate.Day<e.EndDate.Value.Day))||
                    (e.BirthDate.Month<e.EndDate.Value.Month)
                 )
                 );
            return employees.ToList();
        }
    
    }
}
