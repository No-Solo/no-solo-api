using NoSolo.Abstractions.Base;

namespace NoSolo.Core.Entities.FeedBack;

public class FeedBack : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string FeedBackText { get; set; }
}