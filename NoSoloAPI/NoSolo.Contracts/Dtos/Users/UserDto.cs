using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Users.Offers;
using NoSolo.Contracts.Dtos.Users.Photo;
using NoSolo.Contracts.Dtos.Users.Requests;
using NoSolo.Contracts.Dtos.Users.Tags;
using NoSolo.Core.Enums;

namespace NoSolo.Contracts.Dtos.Users;

public record UserDto : BaseDto<Guid>
{
    public required string UserName { get; init; }

    public required string Email { get; init; }

    public required string FirstName { get; init; }
    public string? MiddleName { get; set; }
    public required string LastName { get; init; }

    public required string About { get; init; }
    public required string Description { get; init; }
    public required string Location { get; init; }

    public UserPhotoDto? Photo { get; set; }

    public required LocaleEnum Locale { get; init; }
    public required GenderEnum Gender { get; init; }
    public required SponsorshipEnum Sponsorship { get; init; }

    public List<ContactDto>? Contacts { get; set; }
    public List<UserOfferDto>? Offers { get; set; }
    public List<UserTagDto>? Tags { get; set; }

    public List<MemberDto>? Memberships { get; set; }

    public List<UserRequestDto>? Requests { get; set; }

    public required bool EmailConfirmed { get; init; }
}