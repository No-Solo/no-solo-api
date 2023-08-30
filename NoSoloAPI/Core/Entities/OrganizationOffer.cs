using Core.Enums;

namespace Core.Entities;

public class OrganizationOffer : BaseEntity
{
    public Organization Organization { get; set; }
    public Guid OrganizationId { get; set; }

    public string? Name { get; set; }
    public string Description { get; set; }
    public List<TagEnum> Tags { get; set; } = new();
}