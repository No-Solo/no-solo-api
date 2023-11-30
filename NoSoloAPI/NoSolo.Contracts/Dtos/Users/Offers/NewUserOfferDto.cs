namespace NoSolo.Contracts.Dtos.Users.Offers;

public class NewUserOfferDto
{
    public string Name { get; set; }
    public string Preferences { get; set; }
    public List<string> Tags { get; set; }
}