using BM_API.Data;
using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Repositories
{
    public class BudgetTypeRepository:Repository,IBudgetTypeRepository
    {
        private readonly BMDbContext _bmDbContext;
        public BudgetTypeRepository(BMDbContext bmDbContext):base(bmDbContext)
        {
            _bmDbContext = bmDbContext;
        }
        public async Task<BudgetType> GetBudgetTypeByNameAsync(string name)
        {
            return await _bmDbContext.BudgetTypes.FirstOrDefaultAsync(x => x.Name.Equals(name));
        }
        public async Task<ICollection<BudgetType>> GetBudgetTypesAsync()
        {
            return await _bmDbContext.BudgetTypes.ToListAsync();
        }
        public async Task<ICollection<string>> GetBudgetTypeNamesAsync()
        {
            return await _bmDbContext.BudgetTypes.Select(x => x.Name).ToListAsync();
        }
    }
}
