using Core.Enums;

namespace Core.Entities;

public class Offer : BaseEntity
{
    public string? Name { get; set; }
    public string Descirption { get; set; }

    public List<TagEnum> Tags { get; set; } = new List<TagEnum>();
    
    public Organization Organization { get; set; }
    public Guid OrganizationId { get; set; }
}