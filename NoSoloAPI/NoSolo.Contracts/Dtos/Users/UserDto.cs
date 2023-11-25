using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.Users;

public class UserDto : BaseDto
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public List<OrganizationUserDto> OrganizationUsers { get; set; }
}