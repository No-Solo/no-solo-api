using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.Users.Offers;

public class UserOfferDto : BaseDto
{
    public string Preferences { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
}