using Todoist_API.Models;

namespace Todoist_API.DTOs
{
    public class AddTodoDto
    {
        public string? Title { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? Project { get; set; }
        public Tag[]? Tags { get; set; }
        public string? UserId { get; set; }
    }
}
