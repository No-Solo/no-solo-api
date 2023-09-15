using Core.Interfaces.Data;

namespace Core.Entities;

public class Organization : BaseEntity, IBaseForContact
{
    public string Name { get; set; }
    public string PhotoUrl { get; set; }

    public List<OrganizationOffer> Offers { get; set; } = new();
    public List<OrganizationPhoto> Photos { get; set; } = new();
    public List<OrganizationUser> OrganizationUsers { get; set; } = new();
    public List<Contact<Organization>> Contacts { get; set; } = new();

    public List<Request<Organization, UserOffer>> RequestsFromOrganizationToUserOffer { get; set; } = new();

    public DateTime Created { get; set; } = DateTime.UtcNow;
    
    public Project Project { get; set; }
}