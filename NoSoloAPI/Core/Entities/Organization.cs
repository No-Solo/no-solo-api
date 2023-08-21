﻿namespace Core.Entities;

public class Organization : BaseEntity
{
    public string Name { get; set; }
    public string PhotoUrl { get; set; }

    public List<OrganizationOffer> Offers { get; set; } = new List<OrganizationOffer>();
    public List<OrganizationPhoto> Photos { get; set; } = new List<OrganizationPhoto>();
    public List<OrganizationUser> OrganizationUsers { get; set; } = new List<OrganizationUser>();

    public List<Request<Organization, UserOffer>> RequestsFromOrganizationToUserOffer { get; set; }
    
    public Project Project { get; set; }
}