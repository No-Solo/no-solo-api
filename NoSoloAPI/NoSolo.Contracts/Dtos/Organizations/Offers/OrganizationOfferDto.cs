using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.Organizations.Offers;

public class OrganizationOfferDto : BaseDto
{
    public string? Name { get; set; }
    public string Description { get; set; }
    public List<string> Tags { get; set; }
}