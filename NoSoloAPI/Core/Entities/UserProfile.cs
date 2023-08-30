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
    
    public UserPhoto Photo { get; set; }

    public LocaleEnum Locale { get; set; }
    public GenderEnum Gender { get; set; }

    public List<Contact<UserProfile>> Contacts { get; set; } = new();
    public List<UserOffer> Offers { get; set; } = new();
    public List<UserTag> Tags { get; set; } = new();

    public List<Request<UserProfile, OrganizationOffer>> RequestsFromUserProfileToOgranizationOffer { get; set; }

    public User User { get; set; }
    public Guid UserId { get; set; }
}