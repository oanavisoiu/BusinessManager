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

        public async Task<Company> GetCompanyAsync(User user)
        {
            Company? company = await _bmDbContext.Companies.FirstOrDefaultAsync(x => x.User.Equals(user));
            return company;
        }
        public async Task<Company> GetCompanyByIdAsync(Guid id)
        {
            return await _bmDbContext.Companies.FirstOrDefaultAsync(x=>x.Id==id);
        }
    }
}
