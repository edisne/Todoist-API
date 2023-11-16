using Microsoft.AspNetCore.Identity;

namespace Todoist_API.Models
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
