using BM_API.Models;

namespace BM_API.Repositories.RepositoryInterfaces
{
    public interface IEmployeeRepository:IRepository
    {
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        
    }
}
