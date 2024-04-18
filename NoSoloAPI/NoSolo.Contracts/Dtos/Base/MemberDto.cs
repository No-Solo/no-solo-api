using System.ComponentModel.DataAnnotations;
using NoSolo.Core.Enums;

namespace NoSolo.Contracts.Dtos.Base;

public record MemberDto : BaseDto<Guid>
{
    [Required]
    public required RoleEnum Role { get; init; }
}