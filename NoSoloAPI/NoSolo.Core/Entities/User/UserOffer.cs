using NoSolo.Core.Entities.Base;

namespace NoSolo.Core.Entities.User;

public class UserOffer : BaseEntity
{
    public User User { get; set; }
    public Guid UserGuid { get; set; }

    public string Name { get; set; }
    public string Preferences { get; set; }
    public List<string> Tags { get; set; } = new();
    public DateTime Created { get; set; } = DateTime.UtcNow;
}