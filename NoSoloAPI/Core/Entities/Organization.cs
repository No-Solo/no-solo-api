namespace Core.Entities;

public class Organization : BaseEntity
{
    public string Name { get; set; }
    public string PhotoUrl { get; set; }

    public List<OrganizationOffer> Offers { get; set; } = new();
    public List<OrganizationPhoto> Photos { get; set; } = new();
    public List<OrganizationUser> OrganizationUsers { get; set; } = new();

    public List<Request<Organization, UserOffer>> RequestsFromOrganizationToUserOffer { get; set; }

    public Project Project { get; set; }
}