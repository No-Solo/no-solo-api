using NoSolo.Core.Enums;

namespace NoSolo.Contracts.Dtos.Base;

public class OrganizationUserDto : BaseDto
{
    public RoleEnum Role { get; set; }
}