using Core.Entities;

namespace Core.Specification.Organization.OrganizationPhotoParams;

public class OrganizationPhotoWithSpecificationParams : BaseSpecification<OrganizationPhoto>
{
    public OrganizationPhotoWithSpecificationParams(OrganizationPhotoParams organizationPhotoParams)
        : base(x => (organizationPhotoParams.OrganizationId.HasValue || x.OrganizationId == organizationPhotoParams.OrganizationId))
    {
        ApplyPaging(organizationPhotoParams.PageSize * (organizationPhotoParams.PageNumber -1), organizationPhotoParams.PageSize);
    }
}