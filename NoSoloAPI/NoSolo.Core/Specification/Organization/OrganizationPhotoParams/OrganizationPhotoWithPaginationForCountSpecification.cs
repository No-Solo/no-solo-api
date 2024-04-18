using NoSolo.Core.Entities;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Organization.OrganizationPhotoParams;

public class OrganizationPhotoWithPaginationForCountSpecification : BaseSpecification<OrganizationPhotoEntity>
{
    public OrganizationPhotoWithPaginationForCountSpecification(OrganizationPhotoParams organizationPhotoParams)
    : base(x => (organizationPhotoParams.OrganizationGuid.HasValue || x.OrganizationId == organizationPhotoParams.OrganizationGuid))
    {
        
    }
}