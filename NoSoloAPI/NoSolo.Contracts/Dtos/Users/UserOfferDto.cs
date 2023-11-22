using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.User;

public class UserOfferDto : BaseDto
{
    public string Preferences { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
}