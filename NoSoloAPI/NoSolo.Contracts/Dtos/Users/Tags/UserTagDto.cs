using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.Users.Tags;

public record UserTagDto : BaseDto<Guid>
{
    public required string Tag { get; init; }
    public required bool Active { get; init; }
}