using NoSolo.Core.Enums;

namespace NoSolo.Contracts.Dtos.Base;

public class MemberDto : BaseDto
{
    public RoleEnum Role { get; set; }
}