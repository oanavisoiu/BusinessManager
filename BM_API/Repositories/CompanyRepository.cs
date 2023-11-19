using BM_API.Data;
using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Repositories
{
    public class CompanyRepository : Repository, ICompanyRepository
    {
        private readonly BMDbContext _bmDbContext;
        public CompanyRepository(BMDbContext context):base(context)
        {
            _bmDbContext = context;
        }
        public async Task<List<Company>> GetAllCompaniesAsync()
        {

            return await _bmDbContext.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyByUserIdAsync(string userId)
        {
            return await _bmDbContext.Companies.FirstAsync(x=>x.UserId.Equals(userId));
        }
    }
}
