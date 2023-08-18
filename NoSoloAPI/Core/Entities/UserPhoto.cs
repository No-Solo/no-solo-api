namespace Core.Entities;

public class UserPhoto : Photo
{
    public UserProfile UserProfile { get; set; }
    public Guid UserProfileId { get; set; }
}