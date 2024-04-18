using NoSolo.Core.Entities.Base;
using NoSolo.Core.Enums;

namespace NoSolo.Core.Entities.Organization;

public class OrganizationOfferEntity : BaseEntity<Guid>
{
    public Entities.Organization.OrganizationEntity? Organization { get; set; }
    public required Guid OrganizationId { get; set; }

    public required string Name { get; set; }
    public required string Description { get; set; }
    public List<string>? Tags { get; set; } = new();

    public DateTime? Created { get; set; } = DateTime.UtcNow;
}