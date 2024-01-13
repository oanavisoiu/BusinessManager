namespace BM_API.DTOs.ToDo
{
    public class ToDoDTO
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RecurrenceRule { get; set; }
        public bool AllDay { get; set; }
    }
}
