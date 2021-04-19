using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.Models
{
    public class Email
    {
        [Required]
        public string To { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
