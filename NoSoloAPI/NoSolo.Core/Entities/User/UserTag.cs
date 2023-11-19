using NoSolo.Abstractions.Base;
using NoSolo.Core.Enums;

namespace NoSolo.Core.Entities.User;

public class UserTag : BaseEntity
{
    public TagEnum Tag { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; }

    public UserProfile UserProfile { get; set; }
    public Guid UserProfileId { get; set; }
}