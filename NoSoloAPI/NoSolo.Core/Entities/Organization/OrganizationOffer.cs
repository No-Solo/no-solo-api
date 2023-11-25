﻿using NoSolo.Abstractions.Base;
using NoSolo.Core.Enums;

namespace NoSolo.Core.Entities.Organization;

public class OrganizationOffer : BaseEntity
{
    public Entities.Organization.Organization Organization { get; set; }
    public Guid OrganizationId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public List<TagEnum> Tags { get; set; } = new();

    public DateTime Created { get; set; } = DateTime.UtcNow;
}