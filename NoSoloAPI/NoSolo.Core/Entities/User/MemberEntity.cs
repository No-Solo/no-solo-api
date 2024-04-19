using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Enums;

namespace NoSolo.Core.Entities.User;

public class MemberEntity : BaseEntity<Guid>
{
    public RoleEnum Role { get; set; }

    public UserEntity? UserEntity { get; set; }
    public Guid UserId { get; set; }

    public OrganizationEntity? OrganizationEntity { get; set; }
    public Guid OrganizationId { get; set; }
}