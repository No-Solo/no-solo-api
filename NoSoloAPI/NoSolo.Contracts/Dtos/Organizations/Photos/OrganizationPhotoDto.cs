using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.Organizations.Photos;

public class OrganizationPhotoDto : BaseDto
{
    public bool IsMain { get; set; }
    public string Url { get; set; }
    public string? PublicId { get; set; }
}