namespace Core.Entities;

public class Request : BaseEntity
{
    public List<UserTag> UserTags { get; set; }
    public string Preferences { get; set; }
    
    public Guid UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }
}