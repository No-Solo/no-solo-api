using Core.Specification.BaseSpecification;

namespace Core.Specification.Organization.OrganizationPhotoParams;

public class OrganizationPhotoParams : BasicParams
{
    public Guid? OrganizationId { get; set; }
}