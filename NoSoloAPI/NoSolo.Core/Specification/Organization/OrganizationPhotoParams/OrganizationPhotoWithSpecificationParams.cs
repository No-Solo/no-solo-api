using NoSolo.Core.Entities;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Organization.OrganizationPhotoParams;

public class OrganizationPhotoWithSpecificationParams : BaseSpecification<OrganizationPhotoEntity>
{
    public OrganizationPhotoWithSpecificationParams(OrganizationPhotoParams organizationPhotoParams)
        : base(x => (organizationPhotoParams.OrganizationGuid.HasValue || x.OrganizationId == organizationPhotoParams.OrganizationGuid))
    {
        ApplyPaging(organizationPhotoParams.PageSize * (organizationPhotoParams.PageNumber -1), organizationPhotoParams.PageSize);
    }
}