using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class User : IdentityUser<Guid>
{
    public List<OrganizationUser> OrganizationUsers { get; set; } = new();

    public UserProfile UserProfile { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; } = new();
}