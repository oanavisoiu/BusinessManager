using BM_API.Models;

namespace BM_API.Repositories.RepositoryInterfaces
{
    public interface ICompanyRepository:IRepository
    {
        Task<List<Company>> GetAllCompaniesAsync();
        Task<Company> GetCompanyAsync(User user);
        Task<Company> GetCompanyByIdAsync(Guid id);
    }
}
