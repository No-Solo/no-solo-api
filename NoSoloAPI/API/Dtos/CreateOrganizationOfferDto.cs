using Core.Enums;

namespace API.Dtos;

public class CreateOrganizationOfferDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<TagEnum> Tags { get; set; }
}