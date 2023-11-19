namespace NoSolo.Presentation.Dtos;

public class UserOfferDto : BaseDto
{
    public string Preferences { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
}