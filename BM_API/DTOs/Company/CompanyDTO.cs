using System.ComponentModel.DataAnnotations;

namespace BM_API.DTOs.Company
{
    public class CompanyDTO
    {
        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
