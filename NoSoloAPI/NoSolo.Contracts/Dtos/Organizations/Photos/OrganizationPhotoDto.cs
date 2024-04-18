using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.Organizations.Photos;

public record OrganizationPhotoDto : BaseDto<Guid>
{
    public required bool IsMain { get; init; }
    public required string Url { get; init; }
    public string? PublicId { get; init; }
}