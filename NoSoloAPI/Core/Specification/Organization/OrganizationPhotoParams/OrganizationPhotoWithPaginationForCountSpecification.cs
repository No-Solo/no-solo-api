using Core.Entities;

namespace Core.Specification.Organization.OrganizationPhotoParams;

public class OrganizationPhotoWithPaginationForCountSpecification : BaseSpecification<OrganizationPhoto>
{
    public OrganizationPhotoWithPaginationForCountSpecification(OrganizationPhotoParams organizationPhotoParams)
    : base(x => (organizationPhotoParams.OrganizationId.HasValue || x.OrganizationId == organizationPhotoParams.OrganizationId))
    {
        
    }
}