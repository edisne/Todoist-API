using System.ComponentModel.DataAnnotations;

namespace Todoist_API.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string? Password { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }

    }
}