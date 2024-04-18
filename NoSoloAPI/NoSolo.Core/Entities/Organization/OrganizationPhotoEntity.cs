using NoSolo.Core.Entities.Base;

namespace NoSolo.Core.Entities.Organization;

public class OrganizationPhotoEntity : PhotoEntity
{
    public required bool IsMain { get; set; } = false;

    public Entities.Organization.OrganizationEntity? Organization { get; set; }
    public required Guid OrganizationId { get; set; }
}