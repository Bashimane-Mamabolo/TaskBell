using System.ComponentModel.DataAnnotations;

namespace TaskBell.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        //public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        //public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public DateTime? CompletedAt { get; set; } = null;

    }
}
