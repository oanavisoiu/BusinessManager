using System.ComponentModel.DataAnnotations;

namespace BM_API.Models
{
    public class Company
    {
        [Required]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public virtual User? User { get; set; }
    }
}
