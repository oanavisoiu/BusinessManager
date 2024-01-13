using BM_API.Data;
using BM_API.DTOs.Budget;
using BM_API.Models;

namespace BM_API.Repositories.RepositoryInterfaces
{
    public interface IBudgetRepository:IRepository
    {
        Task<ICollection<Budget>> GetBudgetsByCompanyIdAsync(Guid id);
        Task<Budget> GetBudgetByIdAsync(Guid id);
        Task<ICollection<Budget>> GetUpcomingBudgetsAsync(Guid companyId);
        Task<ICollection<DayBudgetDTO>> Get30DayBudget(Guid companyId);

    }
}
