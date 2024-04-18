using NoSolo.Core.Enums;

namespace NoSolo.Contracts.Dtos.Users.Tags;

public record NewUserTagDto
{
    public required string Tag { get; set; }
    public required bool Active { get; set; }
}