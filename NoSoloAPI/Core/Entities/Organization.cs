namespace Core.Entities;

public class Organization : BaseEntity
{
    public string Name { get; set; }
    public string PhotoUrl { get; set; }

    public List<Offer> Offers { get; set; } = new List<Offer>();
    public List<OrganizationPhoto> Photos { get; set; } = new List<OrganizationPhoto>();
    public List<OrganizationUser> OrganizationUsers { get; set; } = new List<OrganizationUser>();
    
    public Project Project { get; set; }
    public Guid ProjectId { get; set; }
}