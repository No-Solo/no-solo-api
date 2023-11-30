using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.FeedBack;

public class FeedBackDto : BaseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string FeedBackText { get; set; }
}