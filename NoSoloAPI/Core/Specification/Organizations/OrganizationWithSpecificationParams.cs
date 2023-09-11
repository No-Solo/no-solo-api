using Core.Entities;

namespace Core.Specification.Organizations;

public class OrganizationWithSpecificationParams : BaseSpecification<Organization>
{
    public OrganizationWithSpecificationParams(OrganizationParams organizationParams)
        : base(x => string.IsNullOrEmpty(organizationParams.Search) || x.Name.Contains(organizationParams.Search))
    {
        AddOrderBy(x => x.Id);
        
        if (organizationParams.WithContacts)
            AddInclude(x => x.Contacts);
        if (organizationParams.WithOffers)
            AddInclude(x => x.Offers);
        if (organizationParams.WithPhotos)
            AddInclude(x => x.Photos);
        if (organizationParams.WithMembers)
            AddInclude(x => x.OrganizationUsers);
        
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
                    AddOrderBy(p => p.Created);
                    break;
            }
        }

        if (!string.IsNullOrEmpty(organizationParams.SortByDate))
        {
            switch (organizationParams.SortByDate)
            {
                case "dateAsc":
                    AddOrderBy(p => p.Created);
                    break;
                case "dateDesc":
                    AddOrderByDescending(p => p.Created);
                    break;
                default:
                    AddOrderBy(p => p.Created);
                    break;
            }
        }
        
        ApplyPaging(organizationParams.PageSize * (organizationParams.PageNumber -1), organizationParams.PageSize);
    }
}