using NoSolo.Core.Entities.Base;

namespace NoSolo.Core.Entities.Organization;

public class OrganizationPhoto : Photo
{
    public bool IsMain { get; set; }

    public Entities.Organization.Organization Organization { get; set; }
    public Guid OrganizationId { get; set; }
}