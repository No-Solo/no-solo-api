using System.ComponentModel.DataAnnotations;

namespace NoSolo.Contracts.Dtos.Organizations.Organizations;

public record NewOrganizationDto
{
    [MaxLength(128)]
    [MinLength(3)]
    public required string Name { get; set; }
    [MaxLength(256)]
    public required string Description { get; set; }
    public int? NumberOfEmployees { get; set; }
    public string? Address { get; set; }
    public string? WebSiteUri { get; set; }
}