namespace NoSolo.Contracts.Dtos.FeedBack;

public class NewFeedBackDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string FeedBackText { get; set; }
}