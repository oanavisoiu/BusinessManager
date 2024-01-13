using System.ComponentModel.DataAnnotations;

namespace BM_API.Models
{
    public class CompanyEmployee
    {
        public Guid Id { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public Guid EmployeeId { get; set;}
        public virtual Employee? Employee { get; set; }
        public virtual Company? Company { get; set; }
    }
}
