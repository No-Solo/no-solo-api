using NoSolo.Core.Entities;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Organization.OrganizationPhotoParams;

public class OrganizationPhotoWithSpecificationParams : BaseSpecification<OrganizationPhoto>
{
    public OrganizationPhotoWithSpecificationParams(OrganizationPhotoParams organizationPhotoParams)
        : base(x => (organizationPhotoParams.OrganizationId.HasValue || x.OrganizationId == organizationPhotoParams.OrganizationId))
    {
        ApplyPaging(organizationPhotoParams.PageSize * (organizationPhotoParams.PageNumber -1), organizationPhotoParams.PageSize);
    }
}