using System.ComponentModel.DataAnnotations;

namespace BM_API.Models
{
    public class BudgetType
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
