using Core.Entities;
using Core.Enums;

namespace API.Dtos;

public class UserProfileDto : BaseDto
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }

    public string About { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    
    public UserProfilePhotoDto Photo { get; set; }

    public LocaleEnum Locale { get; set; }
    public GenderEnum Gender { get; set; }

    public List<ContactDto> Contacts { get; set; }
    public List<UserOfferDto> Offers { get; set; }
    public List<UserTagDto> Tags { get; set; }
    //
    // public List<Request<UserProfile, OrganizationOffer>> RequestsFromUserProfileToOgranizationOffer { get; set; }
}