﻿using NoSolo.Core.Entities;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Specification.BaseSpecification;
using NoSolo.Core.Specification.OrganizationContact;

namespace NoSolo.Core.Specification.Organization.OrganizationContact;

public class OrganizationContactWithFiltersForCountSpecification : BaseSpecification<Contact<Entities.Organization.Organization>>
{
    public OrganizationContactWithFiltersForCountSpecification(OrganizationContactParams organizationContactParams)
        : base(x => (string.IsNullOrEmpty(organizationContactParams.Search) || x.Text.ToLower().Contains(organizationContactParams.Search)
            && (string.IsNullOrEmpty(organizationContactParams.Search) || x.Type.ToLower().Contains(organizationContactParams.Search))
            && (!organizationContactParams.OrganizationId.HasValue || x.TEntityId == organizationContactParams.OrganizationId)))
    {
        
    }
}