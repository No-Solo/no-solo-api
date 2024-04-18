namespace NoSolo.Contracts.Dtos.Organizations.Offers;

public class NewOrganizationOfferDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public List<string>? Tags { get; set; }
}