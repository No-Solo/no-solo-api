using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.Users.Offers;

public class UserOfferDto : BaseDto
{
    public string Name { get; set; }
    public string Preferences { get; set; }
    public List<string> Tags { get; set; }
}