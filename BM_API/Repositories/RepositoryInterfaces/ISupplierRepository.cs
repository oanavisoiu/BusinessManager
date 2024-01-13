using BM_API.Models;

namespace BM_API.Repositories.RepositoryInterfaces
{
    public interface ISupplierRepository:IRepository
    {
        Task<Supplier> GetSupplierByIdAsync(Guid id);
    }
}
