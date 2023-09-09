namespace Core.Specification;

public class UserProfileParams : BasicParams
{
    private int _pageSize = 6;

    public Guid? UserId { get; set; }

    public bool WithTags { get; set; }
    public bool WithOffers { get; set; }
    public bool WithPhoto { get; set; }
    public bool WithContacts { get; set; }
}