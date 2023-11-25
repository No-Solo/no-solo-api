using NoSolo.Contracts.Dtos.User.Create;

namespace NoSolo.Contracts.Dtos.Users.Tags;

public class UserTagDto : NewUserTagDto
{
    public Guid Id { get; set; }
}