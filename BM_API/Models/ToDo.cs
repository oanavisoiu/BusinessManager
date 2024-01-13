using System.ComponentModel.DataAnnotations;

namespace BM_API.Models
{
    public class ToDo
    {
        public Guid Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        public string RecurrenceRule { get; set; }
        public bool AllDay { get; set; }
        public virtual Company? Company { get; set; }
    }
}
