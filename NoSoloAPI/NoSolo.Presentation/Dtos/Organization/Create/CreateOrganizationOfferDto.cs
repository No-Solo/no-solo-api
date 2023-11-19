using NoSolo.Core.Enums;

namespace NoSolo.Presentation.Dtos.Organization.Create;

public class CreateOrganizationOfferDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<TagEnum> Tags { get; set; }
}