using BM_API.Data;
using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Repositories
{
    public class SupplierRepository:Repository,ISupplierRepository
    {
        private readonly BMDbContext _bmDbContext;

        public SupplierRepository(BMDbContext bmDbContext) : base(bmDbContext)
        {
            _bmDbContext = bmDbContext;
        }

        public async Task<Supplier> GetSupplierByIdAsync(Guid id)
        {
            return await _bmDbContext.Suppliers.FirstAsync(x => x.Id.Equals(id));
        }
    }
}
