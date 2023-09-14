using Core.Enums;

namespace API.Dtos;

public class UserTagDto : CreateUserTagDto
{
    public Guid Id { get; set; }
}