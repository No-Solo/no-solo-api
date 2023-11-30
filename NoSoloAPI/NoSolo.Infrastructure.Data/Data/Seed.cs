using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Data.Data;

public class Seed
{
    public static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        // var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();

        var roles = new List<UserRole>
        {
            new UserRole { Name = "RegisteredUser" },
            new UserRole { Name = "Admin" },
            new UserRole { Name = "Moderator" },
        };

        foreach (var role in roles)
        {
            var roleExists = await roleManager.RoleExistsAsync(role.Name);
            if (!roleExists)
            {
                await roleManager.CreateAsync(role);
            }
        }

        // var admin = new User()
        // {
        //     UserName = "Temp123",
        //     Email = "kirillnester5@gmail.com"
        // };
        //
        // await userManager.CreateAsync(admin, "Temp123@");
        // await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
    }
}