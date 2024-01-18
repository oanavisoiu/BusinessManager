namespace BM_API.DTOs.Budget
{
    public class UpcomingBudgetDTO
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string PaymentTypeName { get; set; }
        public double Value { get; set; }
    }
}
