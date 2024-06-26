﻿using NoSolo.Core.Enums;
using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Organization.Organization;

public class OrganizationWithSpecificationParams : BaseSpecification<Entities.Organization.OrganizationEntity>
{
    public OrganizationWithSpecificationParams(OrganizationParams organizationParams)
        : base(x => string.IsNullOrEmpty(organizationParams.Search) || x.Name.Contains(organizationParams.Search) &&
            (organizationParams.UserGuid.HasValue || x.OrganizationUsers.Exists(entity => entity.UserId == organizationParams.UserGuid)) &&
            (organizationParams.OrganizationGuid.HasValue || x.Id == organizationParams.OrganizationGuid))
    {
        AddOrderBy(x => x.Id);

        if (organizationParams.Includes is not null && organizationParams.Includes.Count > 0)
            foreach (var include in organizationParams.Includes)
            {
                ParseInclude(include);
            }
        
        if (!string.IsNullOrEmpty(organizationParams.SortByAlphabetical))
        {
            switch (organizationParams.SortByAlphabetical)
            {
                case "alphaAsc":
                    AddOrderBy(p => p.Name);
                    break;
                case "alphaDesc":
                    AddOrderByDescending(p => p.Name);
                    break;
                default:
                    AddOrderBy(p => p.DateCreated);
                    break;
            }
        }

        if (!string.IsNullOrEmpty(organizationParams.SortByDate))
        {
            switch (organizationParams.SortByDate)
            {
                case "dateAsc":
                    AddOrderBy(p => p.DateCreated);
                    break;
                case "dateDesc":
                    AddOrderByDescending(p => p.DateCreated);
                    break;
                default:
                    AddOrderBy(p => p.DateCreated);
                    break;
            }
        }

        ApplyPaging(organizationParams.PageSize * (organizationParams.PageNumber - 1), organizationParams.PageSize);
    }

    private void ParseInclude(OrganizationIncludeEnum include)
    {
        switch (include)
        {
            case OrganizationIncludeEnum.Contacts:
                AddInclude(c => c.Contacts);
                break;
            case OrganizationIncludeEnum.Members:
                AddInclude(m => m.OrganizationUsers);
                break;
            case OrganizationIncludeEnum.Offers:
                AddInclude(o => o.Offers);
                break;
            case OrganizationIncludeEnum.Photos:
                AddInclude(p => p.Photos);
                break;
        }
    }
}