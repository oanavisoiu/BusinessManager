using System.ComponentModel.DataAnnotations;

namespace BM_API.DTOs.Supplier
{
    public class SupplierDTO
    {
        [Required]
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
        public string? Link { get; set; }
    }
}
