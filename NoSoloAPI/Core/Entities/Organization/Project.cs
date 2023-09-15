namespace Core.Entities;

public class Project : BaseEntity
{
    public string? Name { get; set; }
    public string Description { get; set; }

    public Organization Organization { get; set; }
    public Guid OrganizationId { get; set; }
}