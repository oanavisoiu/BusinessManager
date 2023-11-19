using System.ComponentModel.DataAnnotations;

namespace BM_API.Models
{
    public class CompanyEmployee
    {
        [Required] 
        public Guid Id { get; set; }
        [Required]
        public string CompanyId { get; set; }
        [Required]
        public string EmployeeId { get; set;}
    }
}
