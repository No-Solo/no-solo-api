using Core.Enums;

namespace Core.Entities;

public class OrganizationUser : BaseEntity
{
    public RoleEnum Role { get; set; }

    public User User { get; set; }
    public Guid UserId { get; set; }

    public Organization Organization { get; set; }
    public Guid OrganizationId { get; set; }
}