using System.ComponentModel.DataAnnotations;

namespace BM_API.Models
{
    public class Supplier
    {
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Link { get; set; }
    }
}
