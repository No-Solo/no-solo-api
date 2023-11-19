using NoSolo.Core.Entities.Base;

namespace NoSolo.Core.Entities.User;

public class UserPhoto : Photo
{
    public UserProfile UserProfile { get; set; }
    public Guid UserProfileId { get; set; }
}