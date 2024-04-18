using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;
using NoSolo.Core.Entities.Auth;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Enums;

namespace NoSolo.Core.Entities.User;

public class UserEntity : IdentityUser<Guid>, IBaseForContact
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }

    public string About { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }

    public UserPhotoEntity? Photo { get; set; } = null;

    public LocaleEnum Locale { get; set; } = LocaleEnum.English;
    public GenderEnum Gender { get; set; }
    public SponsorshipEnum Sponsorship { get; set; } = SponsorshipEnum.Zero;

    public List<ContactEntity<UserEntity>> Contacts { get; set; } = new();
    public List<UserOfferEntity> Offers { get; set; } = new();
    public List<UserTagEntity> Tags { get; set; } = new();

    public List<RequestEntity<UserEntity, OrganizationOfferEntity>> RequestsFromUserProfileToOgranizationOffer { get; set; } = new();

    public List<MemberEntity> OrganizationUsers { get; set; } = new();

    public List<RefreshToken> RefreshTokens { get; set; } = new();

    public Collection<UserRoleEntity> UserRoles { get; set; } = new();
}