using BM_API.Models;

namespace BM_API.Repositories.RepositoryInterfaces
{
    public interface IPaymentTypeRepository:IRepository
    {
        Task<PaymentType> GetPaymentTypeByNameAsync(string name);
        Task<ICollection<PaymentType>> GetPaymentTypesAsync();
        Task<ICollection<string>> GetPaymentTypeNamesAsync();
    }

}
