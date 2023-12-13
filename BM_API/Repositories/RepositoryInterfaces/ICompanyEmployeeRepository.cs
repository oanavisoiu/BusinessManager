using BM_API.Models;

namespace BM_API.Repositories.RepositoryInterfaces
{
    public interface ICompanyEmployeeRepository:IRepository
    {
        Task<CompanyEmployee> GetCompanyEmployeeByEmployeeId(Guid id);
        Task<ICollection<CompanyEmployee>> GetEmployeesByCompanyAsync(Guid companyId);
        Task<long> GetSumOfSalariesAsync(Guid companyId);
    }
}
