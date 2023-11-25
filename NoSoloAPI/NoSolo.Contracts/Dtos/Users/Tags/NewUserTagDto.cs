using NoSolo.Core.Enums;

namespace NoSolo.Contracts.Dtos.Users.Tags;

public class NewUserTagDto
{
    public string Tag { get; set; }
    public bool Active { get; set; }
}