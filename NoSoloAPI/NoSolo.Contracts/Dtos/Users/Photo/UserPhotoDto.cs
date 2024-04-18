using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.Users.Photo;

public record UserPhotoDto : BaseDto<Guid>
{
    public required string Url { get; init; }
    public string? PublicId { get; set; }
}