namespace NoSolo.Contracts.Dtos.Organizations.Organizations;

public class NewOrganizationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int? NumberOfEmployees { get; set; }
    public string? Address { get; set; }
    public string? WebSiteUrl { get; set; }
}