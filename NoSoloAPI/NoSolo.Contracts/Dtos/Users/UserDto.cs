using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.User;
using NoSolo.Contracts.Dtos.Users.Photo;
using NoSolo.Contracts.Dtos.Users.Requests;
using NoSolo.Contracts.Dtos.Users.Tags;
using NoSolo.Core.Enums;

namespace NoSolo.Contracts.Dtos.Users;

public class UserDto : BaseDto
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }

    public string About { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }

    public UserPhotoDto Photo { get; set; }

    public LocaleEnum Locale { get; set; }
    public GenderEnum Gender { get; set; }
    public SponsorshipEnum Sponsorship { get; set; }

    public List<ContactDto> Contacts { get; set; }
    public List<UserOfferDto> Offers { get; set; }
    public List<UserTagDto> Tags { get; set; }

    public List<MemberDto> Memberships { get; set; }

    public List<UserRequestDto> Requests { get; set; }

    public bool EmailConfirmed { get; set; }
}