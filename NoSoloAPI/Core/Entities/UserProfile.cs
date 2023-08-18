using Core.Enums;

namespace Core.Entities;

public class UserProfile : BaseEntity
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }

    public string About { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }

    public string PhotoUrl { get; set; }
    public UserPhoto Photo { get; set; }
    public Guid PhotoId { get; set; }
    
    public LocaleEnum Locale { get; set; }
    public GenderEnum Gender { get; set; }

    public List<Contact<UserProfile>> Contacts { get; set; } = new List<Contact<UserProfile>>();
    public List<Request> Requests { get; set; } = new List<Request>();
    public List<UserTag> Tags { get; set; } = new List<UserTag>();

    public User User { get; set; }
    public Guid UserId { get; set; }
}