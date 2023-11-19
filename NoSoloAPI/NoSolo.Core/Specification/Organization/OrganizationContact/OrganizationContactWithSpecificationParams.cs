using NoSolo.Core.Entities;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Specification.BaseSpecification;
using NoSolo.Core.Specification.OrganizationContact;

namespace NoSolo.Core.Specification.Organization.OrganizationContact;

public class OrganizationContactWithSpecificationParams : BaseSpecification<Contact<Entities.Organization.Organization>>
{
    public OrganizationContactWithSpecificationParams(OrganizationContactParams organizationContactParams)
        : base(x => (string.IsNullOrEmpty(organizationContactParams.Search) || x.Text.ToLower().Contains(organizationContactParams.Search)
            && (string.IsNullOrEmpty(organizationContactParams.Search) || x.Type.ToLower().Contains(organizationContactParams.Search))
            && (!organizationContactParams.OrganizationId.HasValue || x.TEntityId == organizationContactParams.OrganizationId)))
    {
        if (!string.IsNullOrEmpty(organizationContactParams.SortByAlphabetical))
        {
            switch (organizationContactParams.SortByAlphabetical)
            {
                case "alphaAsc":
                    AddOrderBy(p => p.Text);
                    break;
                case "alphaDesc":
                    AddOrderByDescending(p => p.Text);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }
        }
        
        ApplyPaging(organizationContactParams.PageSize * (organizationContactParams.PageNumber -1), organizationContactParams.PageSize);
    }
}