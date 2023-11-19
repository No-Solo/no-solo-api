using NoSolo.Abstractions.Base;

namespace NoSolo.Core.Entities.Auth;

public class RefreshToken : BaseEntity
{
    public string TokenHash { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public Guid UserId { get; set; }
    public User.User User { get; set; }
}