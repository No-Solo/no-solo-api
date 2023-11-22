using NoSolo.Contracts.Dtos.User.Create;

namespace NoSolo.Contracts.Dtos.User;

public class UserTagDto : CreateUserTagDto
{
    public Guid Id { get; set; }
}