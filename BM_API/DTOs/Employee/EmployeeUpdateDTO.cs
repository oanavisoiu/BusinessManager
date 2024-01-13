using System.ComponentModel.DataAnnotations;

namespace BM_API.DTOs.EmployeeUpdateDto
{
    public class EmployeeUpdateDTO
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "First name must be at least {2} characters and max {1} characters.")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Last name must be at least {2} characters and max {1} characters.")]
        public string? LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        public long Salary { get; set; }

        [Required]
        public string? Department { get; set; }
    }

}

