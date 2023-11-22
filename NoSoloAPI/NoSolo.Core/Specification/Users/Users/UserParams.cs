namespace NoSolo.Core.Specification.Users.Users;

public class UserParams
{
    public string? Email { get; set; }

    public bool UserProfileInclude { get; set; }
    public bool OrganizationsInclude { get; set; }
}