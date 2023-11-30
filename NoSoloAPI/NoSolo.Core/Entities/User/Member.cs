using NoSolo.Core.Entities.Base;
using NoSolo.Core.Enums;

namespace NoSolo.Core.Entities.User;

public class Member : BaseEntity
{
    public RoleEnum Role { get; set; }

    public Entities.User.User User { get; set; }
    public Guid UserId { get; set; }

    public Entities.Organization.Organization Organization { get; set; }
    public Guid OrganizationId { get; set; }
}