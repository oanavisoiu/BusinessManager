using BM_API.Models;

namespace BM_API.Repositories.RepositoryInterfaces
{
    public interface ICompanySupplierRepository : IRepository
    {
        Task<CompanySupplier> GetCompanySupplierByIdAsync(Guid id);
        Task<List<CompanySupplier>> GetCompanySuppliersByCompanyIdAsync(Guid companyId);
        Task<CompanySupplier> GetCompalySupplierBySupplierIdAsync(Guid supplierId);
    }
}
