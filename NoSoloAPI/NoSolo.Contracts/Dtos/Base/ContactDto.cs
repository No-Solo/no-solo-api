namespace NoSolo.Contracts.Dtos.Base;

public class ContactDto : BaseDto
{
    public string Type { get; set; }
    public string Url { get; set; }
    public string Text { get; set; }
}