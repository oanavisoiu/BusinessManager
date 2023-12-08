using System.ComponentModel.DataAnnotations;

namespace BM_API.Models
{
    public class CompanySupplier
    {
        public Guid Id { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public Guid SupplierId { get; set; }
        public virtual Company? Company { get; set; }
        public virtual Supplier? Supplier { get; set; }
    }
}
