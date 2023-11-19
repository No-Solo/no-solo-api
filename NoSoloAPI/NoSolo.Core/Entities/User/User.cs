using Microsoft.AspNetCore.Identity;
using NoSolo.Core.Entities.Auth;
using NoSolo.Core.Entities.Organization;

namespace NoSolo.Core.Entities.User;

public class User : IdentityUser<Guid>
{
    public List<OrganizationUser> OrganizationUsers { get; set; } = new();

    public UserProfile UserProfile { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; } = new();
}