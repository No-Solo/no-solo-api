using NoSolo.Abstractions.Base;
using NoSolo.Core.Enums;

namespace NoSolo.Core.Entities.User;

public class UserTag : BaseEntity
{
    public string Tag { get; set; }
    public bool Active { get; set; }

    public User User { get; set; }
    public Guid UserGuid { get; set; }
}