using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.Organizations.Organizations;

public record UpdateOrganizationDto : BaseCreatedDto<Guid>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int? NumberOfEmployees { get; set; }
    public string? Address { get; set; }
    public string? WebSiteUrl { get; set; }
}