using Core.Enums;

namespace API.Dtos;

public class CreateUserTagDto
{
    public TagEnum Tag { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; }
}