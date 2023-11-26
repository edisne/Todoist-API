using System.Collections;
using Todoist_API.DTOs.Tags;
using Todoist_API.Models;

namespace Todoist_API.DTOs.Todos
{
    public class AddTodoDto
    {
        public string? Title { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? Project { get; set; }
        public ICollection<AddTagDto> Tags { get; set; }
        public string? UserId { get; set; }
    }
}
