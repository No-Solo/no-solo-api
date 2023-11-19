using NoSolo.Core.Entities;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Organization.OrganizationPhotoParams;

public class OrganizationPhotoWithPaginationForCountSpecification : BaseSpecification<OrganizationPhoto>
{
    public OrganizationPhotoWithPaginationForCountSpecification(OrganizationPhotoParams organizationPhotoParams)
    : base(x => (organizationPhotoParams.OrganizationId.HasValue || x.OrganizationId == organizationPhotoParams.OrganizationId))
    {
        
    }
}