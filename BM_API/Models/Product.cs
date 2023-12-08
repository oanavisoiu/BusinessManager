using System.ComponentModel.DataAnnotations;

namespace BM_API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public Guid SupplierId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Link { get; set; }
        [Required]
        public int Price { get; set; }
        public virtual Supplier? Supplier { get; set; }
    }
}
