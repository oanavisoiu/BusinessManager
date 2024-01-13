using System.ComponentModel.DataAnnotations;

namespace BM_API.DTOs.CompanySupplier
{
    public class CompanySupplierDTO
    {
        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public Guid SupplierId { get; set; }
    }
}
