using Core.Enums;

namespace API.Dtos;

public class OrganizationUserDto : BaseDto
{
    public RoleEnum Role { get; set; }
}