using NoSolo.Core.Enums;

namespace NoSolo.Presentation.Dtos.User.Create;

public class CreateUserTagDto
{
    public TagEnum Tag { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; }
}