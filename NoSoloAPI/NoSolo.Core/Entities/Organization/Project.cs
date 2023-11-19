using NoSolo.Abstractions.Base;

namespace NoSolo.Core.Entities.Organization;

public class Project : BaseEntity
{
    public string? Name { get; set; }
    public string Description { get; set; }

    public Entities.Organization.Organization Organization { get; set; }
    public Guid OrganizationId { get; set; }
}