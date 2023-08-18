namespace Core.Entities;

public class OrganizationPhoto : Photo
{
    public bool IsMain { get; set; }

    public Organization Organization { get; set; }
    public Guid OrganizationId { get; set; }
}