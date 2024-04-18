using NoSolo.Core.Entities.Base;

namespace NoSolo.Core.Entities.User;

public class UserOfferEntity : BaseEntity<Guid>
{
    public UserEntity? UserEntity { get; set; }
    public required Guid UserGuid { get; set; }

    public required string Name { get; set; }
    public required string Preferences { get; set; }
    public List<string>? Tags { get; set; } = new();
    public DateTime? Created { get; set; } = DateTime.UtcNow;
}