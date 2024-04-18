using NoSolo.Core.Entities.Base;

namespace NoSolo.Core.Entities.User;

public class UserPhotoEntity : PhotoEntity
{
    public UserEntity? User { get; set; }
    public required Guid UserGuid { get; set; }
}