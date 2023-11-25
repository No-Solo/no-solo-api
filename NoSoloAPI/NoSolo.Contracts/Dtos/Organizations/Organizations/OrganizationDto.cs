using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Contracts.Dtos.Organizations.Photos;

namespace NoSolo.Contracts.Dtos.Organizations.Organizations;

public class OrganizationDto : BaseDto
{
    public string Name { get; set; }
    public string PhotoUrl { get; set; }

    public List<OrganizationOfferDto> Offers { get; set; }
    public List<OrganizationPhotoDto> Photos { get; set; }
    public List<OrganizationUserDto> OrganizationUsers { get; set; }
    public List<ContactDto> Contacts { get; set; } = new();
    
    // public List<Request<Organization, UserOffer>> RequestsFromOrganizationToUserOffer { get; set; }

    public DateTime Created { get; set; }
}