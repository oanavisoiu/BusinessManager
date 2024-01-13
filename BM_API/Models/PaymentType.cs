using System.ComponentModel.DataAnnotations;

namespace BM_API.Models
{
    public class PaymentType
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
