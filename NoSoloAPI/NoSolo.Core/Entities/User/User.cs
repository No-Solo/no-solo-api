using Microsoft.AspNetCore.Identity;
using NoSolo.Abstractions.Base;
using NoSolo.Core.Entities.Auth;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Enums;

namespace NoSolo.Core.Entities.User;

public class User : IdentityUser<Guid>, IBaseForContact
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }

    public string About { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    
    public UserPhoto Photo { get; set; }

    public LocaleEnum Locale { get; set; } = LocaleEnum.English;
    public GenderEnum Gender { get; set; }
    public SponsorshipEnum Sponsorship { get; set; } = SponsorshipEnum.Zero;

    public List<Contact<User>> Contacts { get; set; } = new();
    public List<UserOffer> Offers { get; set; } = new();
    public List<UserTag> Tags { get; set; } = new();

    public List<Request<User, OrganizationOffer>> RequestsFromUserProfileToOgranizationOffer { get; set; } = new();
    
    public List<Member> OrganizationUsers { get; set; } = new();

    public List<RefreshToken> RefreshTokens { get; set; } = new();
}