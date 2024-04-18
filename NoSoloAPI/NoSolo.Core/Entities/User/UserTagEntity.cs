using NoSolo.Core.Entities.Base;
using NoSolo.Core.Enums;
using UserEntity = NoSolo.Core.Entities.User.UserEntity;

namespace NoSolo.Core.Entities.User;

public class UserTagEntity : BaseEntity<Guid>
{
    public required string Tag { get; set; }
    public required bool Active { get; set; }

    public UserEntity? User { get; set; }
    public required Guid UserGuid { get; set; }
}