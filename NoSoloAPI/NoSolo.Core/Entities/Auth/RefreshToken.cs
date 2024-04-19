using NoSolo.Core.Entities.Base;

namespace NoSolo.Core.Entities.Auth;

public class RefreshToken : BaseEntity<Guid>
{
    public required string TokenHash { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public Guid UserId { get; set; }
    public User.UserEntity? UserEntity { get; set; }
}