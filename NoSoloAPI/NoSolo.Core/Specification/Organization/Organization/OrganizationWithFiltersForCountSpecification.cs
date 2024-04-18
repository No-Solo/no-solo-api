using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Organization.Organization;

public class OrganizationWithFiltersForCountSpecification : BaseSpecification<Entities.Organization.OrganizationEntity>
{
    public OrganizationWithFiltersForCountSpecification(OrganizationParams organizationParams)
        : base(x => string.IsNullOrEmpty(organizationParams.Search) || x.Name.Contains(organizationParams.Search) &&
            (organizationParams.UserGuid.HasValue || x.OrganizationUsers.Exists(x => x.UserId == organizationParams.UserGuid)) &&
            (organizationParams.OrganizationGuid.HasValue || x.Id == organizationParams.OrganizationGuid))
    {
        
    }
}