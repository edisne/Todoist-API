using Microsoft.AspNetCore.Identity;

namespace Todoist_API.Models;

public class User : IdentityUser
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    public List<Todo>? Todos { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}
