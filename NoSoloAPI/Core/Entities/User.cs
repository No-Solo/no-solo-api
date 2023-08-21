namespace Core.Entities;

public class User : BaseEntity
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public List<OrganizationUser> OrganizationUsers { get; set; } = new List<OrganizationUser>();
    
    public UserProfile UserProfile { get; set; }
}