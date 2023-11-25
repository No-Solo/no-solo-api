using NoSolo.Core.Entities.Base;

namespace NoSolo.Core.Entities.User;

public class UserPhoto : Photo
{
    public User User { get; set; }
    public Guid UserGuid { get; set; }
}