using System.Text.Json.Serialization;

namespace Todoist_API.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        
        [JsonIgnore]
        public Todo Todo { get; set; }


    }
}
