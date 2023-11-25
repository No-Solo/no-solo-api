using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Organization.OrganizationPhotoParams;

public class OrganizationPhotoParams : BasicParams
{
    public Guid? OrganizationGuid { get; set; }
}