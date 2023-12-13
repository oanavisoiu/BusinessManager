using System.ComponentModel.DataAnnotations;

namespace BM_API.Models
{
    public class Budget
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public decimal Value { get; set; }
        [Required]
        public string PaymentTypeName { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
    }
}
