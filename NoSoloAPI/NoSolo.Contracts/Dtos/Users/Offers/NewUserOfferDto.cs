namespace NoSolo.Contracts.Dtos.Users.Offers;

public record NewUserOfferDto
{
    public required string Name { get; set; }
    public required string Preferences { get; set; }
    public List<string>? Tags { get; set; }
}