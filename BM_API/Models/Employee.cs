using System.ComponentModel.DataAnnotations;

namespace BM_API.Models
{
    public class Employee
    {
       public Guid Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public double Salary { get; set; }
        [Required]
        public string? Department { get; set; }
    }
}
