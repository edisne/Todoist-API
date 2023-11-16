using Microsoft.AspNetCore.Identity;

namespace Todoist_API.Data;

public class Seed
{
    public static async Task SeedRoles (UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var roles = new List<Role>()
        {
            new Role {Name = "Member"},
            new Role {Name = "Admin"},
            new Role {Name = "Moderator"}
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        var admin = new User
        {
            UserName = "admin"
        };

        await userManager.CreateAsync(admin, "1Password");
        await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator"});
    }
}