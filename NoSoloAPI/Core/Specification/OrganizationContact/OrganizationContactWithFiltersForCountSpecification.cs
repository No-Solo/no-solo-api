using Core.Entities;

namespace Core.Specification.OrganizationContact;

public class OrganizationContactWithFiltersForCountSpecification : BaseSpecification<Contact<Organization>>
{
    public OrganizationContactWithFiltersForCountSpecification(OrganizationContactParams organizationContactParams)
        : base(x => (string.IsNullOrEmpty(organizationContactParams.Search) || x.Text.ToLower().Contains(organizationContactParams.Search)
            && (string.IsNullOrEmpty(organizationContactParams.Search) || x.Type.ToLower().Contains(organizationContactParams.Search))
            && (!organizationContactParams.OrganizationId.HasValue || x.TEntityId == organizationContactParams.OrganizationId)))
    {
        
    }
}