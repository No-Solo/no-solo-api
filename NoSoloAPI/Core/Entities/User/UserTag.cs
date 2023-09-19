using Core.Entities.User;
using Core.Enums;

namespace Core.Entities;

public class UserTag : BaseEntity
{
    public TagEnum Tag { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; }

    public UserProfile UserProfile { get; set; }
    public Guid UserProfileId { get; set; }
}