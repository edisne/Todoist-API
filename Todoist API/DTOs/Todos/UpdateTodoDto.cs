using Todoist_API.DTOs.Tags;

namespace Todoist_API.DTOs.Todos
{
    public class UpdateTodoDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? Project { get; set; }
        public ICollection<UpdateTagDto> Tags { get; set; }
        public string? UserId { get; set; }
    }
}
