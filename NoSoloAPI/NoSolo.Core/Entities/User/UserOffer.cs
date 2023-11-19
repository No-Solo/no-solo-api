using NoSolo.Abstractions.Base;

namespace NoSolo.Core.Entities.User;

public class UserOffer : BaseEntity
{
    public UserProfile UserProfile { get; set; }
    public Guid UserProfileId { get; set; }

    public string Preferences { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
}