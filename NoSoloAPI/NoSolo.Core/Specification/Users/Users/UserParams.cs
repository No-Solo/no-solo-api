using NoSolo.Core.Enums;

namespace NoSolo.Core.Specification.Users.Users;

public class UserParams
{
    public string? Email { get; set; }
    public List<UserInclude> Includes { get; set; }
}