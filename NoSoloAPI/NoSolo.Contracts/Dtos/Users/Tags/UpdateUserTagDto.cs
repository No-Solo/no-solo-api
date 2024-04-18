using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.Users.Tags;

public record UpdateUserTagDto : BaseDto<Guid>
{
    public required string Tag { get; set; }
    public required bool Active { get; set; }
}