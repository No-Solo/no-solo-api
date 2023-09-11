using Core.Entities;

namespace API.Dtos;

public class UserDto : BaseDto
{
    public string UserName { get; set; }

    public string Email { get; set; }
    
    public UserProfileDto UserProfile { get; set; }

    public List<OrganizationUserDto> OrganizationUsers { get; set; }
}