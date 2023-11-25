using NoSolo.Abstractions.Base;

namespace NoSolo.Core.Entities.User;

public class UserOffer : BaseEntity
{
    public User User { get; set; }
    public Guid UserGuid { get; set; }

    public string Preferences { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
}