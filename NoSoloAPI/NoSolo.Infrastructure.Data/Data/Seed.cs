using NoSolo.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Data.Data.Migrations;

public class Seed
{
    public static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();
        
        var roles = new List<UserRole>
        {
            new UserRole{Name = "RegisteredUser"},
            new UserRole{Name = "Admin"},
            new UserRole{Name = "Moderator"},
        };

        foreach (var role in roles)
        {
            var roleExists = await roleManager.RoleExistsAsync(role.Name);
            if (!roleExists)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}