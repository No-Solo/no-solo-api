﻿using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.User;

namespace NoSolo.Core.Entities.Organization;

public class Organization : BaseEntity, IBaseForContact
{
    public string Name { get; set; }
    public string? PhotoUrl { get; set; }
    public string Description { get; set; }
    public int? NumberOfEmployees { get; set; }
    public string? Address { get; set; }
    public string? WebSiteUrl { get; set; }

    public List<OrganizationOffer> Offers { get; set; } = new();
    public List<OrganizationPhoto> Photos { get; set; } = new();
    public List<Member> OrganizationUsers { get; set; } = new();
    public List<Contact<Organization>> Contacts { get; set; } = new();

    public List<Request<Organization, UserOffer>> RequestsFromOrganizationToUserOffer { get; set; } = new();

    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}