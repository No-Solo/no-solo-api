using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.Organizations.Offers;

public record OrganizationOfferDto : BaseDto<Guid>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public List<string>? Tags { get; set; }
}