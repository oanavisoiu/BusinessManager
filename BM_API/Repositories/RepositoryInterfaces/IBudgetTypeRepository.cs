using BM_API.Models;

namespace BM_API.Repositories.RepositoryInterfaces
{
    public interface IBudgetTypeRepository:IRepository
    {
        Task<BudgetType> GetBudgetTypeByNameAsync(string name);
        Task<ICollection<BudgetType>> GetBudgetTypesAsync();
        Task<ICollection<string>> GetBudgetTypeNamesAsync();
    }
}
