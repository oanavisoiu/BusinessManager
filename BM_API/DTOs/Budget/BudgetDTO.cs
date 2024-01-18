using BM_API.Models;
using System.ComponentModel.DataAnnotations;

namespace BM_API.DTOs.Budget
{
    public class BudgetDTO
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public double Value { get; set; }
        public string PaymentTypeName { get; set; }
        public string BudgetTypeName { get; set; }
    }
}
