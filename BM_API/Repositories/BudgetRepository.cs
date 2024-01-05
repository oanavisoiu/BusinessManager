using BM_API.Data;
using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Repositories
{
    public class BudgetRepository:Repository,IBudgetRepository
    {
        private readonly BMDbContext _bmDbContext;
        public BudgetRepository(BMDbContext bmDbContext):base(bmDbContext)
        {
            _bmDbContext = bmDbContext;
        }
        public async Task<ICollection<Budget>> GetBudgetsByCompanyIdAsync(Guid id)
        {
            return _bmDbContext.Budgets.Where(x => x.CompanyId.Equals(id)).ToList();
        }

        public async Task<Budget> GetBudgetByIdAsync(Guid id)
        {
            return await _bmDbContext.Budgets.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        public async Task<ICollection<Budget>> GetUpcomingBudgetsAsync(Guid companyId)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddMonths(1);
            var budgets = _bmDbContext.Budgets.Where(b=>(b.CompanyId.Equals(companyId))&&((b.Date.Day>= startDate.Day && b.Date.Month == startDate.Month)||
            (b.Date.Day <= endDate.Day && b.Date.Month == endDate.Month)||
            (b.Date.Month > startDate.Month &&b.Date.Month < endDate.Month))).ToList();
            return budgets;
        }

    }
}
