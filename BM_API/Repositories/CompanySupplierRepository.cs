using BM_API.Data;
using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Repositories
{
    public class CompanySupplierRepository : Repository, ICompanySupplierRepository
    {
        private readonly BMDbContext _dbContext;
        public CompanySupplierRepository(BMDbContext bMDbContext):base(bMDbContext)
        {
            _dbContext = bMDbContext;
        }
        public async Task<CompanySupplier> GetCompanySupplierByIdAsync(Guid id)
        {
            return await _dbContext.CompanySuppliers.FirstAsync(x => x.Id.Equals(id));
        }
        public async Task<List<CompanySupplier>> GetCompanySuppliersByCompanyIdAsync(Guid companyId)
        {
            return await _dbContext.CompanySuppliers.Where(x => x.CompanyId.Equals(companyId)).ToListAsync();
        }
        public async Task<CompanySupplier> GetCompanySupplierBySupplierIdAsync(Guid supplierId)
        {
            return await _dbContext.CompanySuppliers.FirstOrDefaultAsync(x => x.SupplierId.Equals(supplierId));
        }
    }
}
