using BM_API.Data;
using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Repositories
{
    public class PaymentTypeRepository:Repository,IPaymentTypeRepository
    {
        private readonly BMDbContext _bmDbContext;
        public PaymentTypeRepository(BMDbContext bmDbContext):base(bmDbContext)
        {
            _bmDbContext = bmDbContext;
        }
        public async Task<PaymentType> GetPaymentTypeByNameAsync(string name)
        {
            return await _bmDbContext.PaymentTypes.FirstOrDefaultAsync(x => x.Name.Equals(name));
        }
        public async Task<ICollection<PaymentType>> GetPaymentTypesAsync()
        {
            return await _bmDbContext.PaymentTypes.Distinct().ToListAsync();
        }
        public async Task<ICollection<string>> GetPaymentTypeNamesAsync()
        {
            return await _bmDbContext.PaymentTypes.Select(x => x.Name).ToListAsync();
        }
    }
}
