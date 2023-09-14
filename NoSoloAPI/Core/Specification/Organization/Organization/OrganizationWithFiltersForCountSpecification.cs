using Core.Entities;

namespace Core.Specification.Organizations;

public class OrganizationWithFiltersForCountSpecification : BaseSpecification<Entities.Organization>
{
    public OrganizationWithFiltersForCountSpecification(OrganizationParams organizationParams)
        : base(x => string.IsNullOrEmpty(organizationParams.Search) || x.Name.Contains(organizationParams.Search))
    {
        
    }
}