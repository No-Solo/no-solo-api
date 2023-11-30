using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Contracts.Dtos.Organizations.Photos;
using NoSolo.Contracts.Dtos.Organizations.Requests;

namespace NoSolo.Contracts.Dtos.Organizations.Organizations;

public class OrganizationDto : BaseDto
{
    public string Name { get; set; }
    public string PhotoUrl { get; set; }

    public List<OrganizationOfferDto> Offers { get; set; }
    public List<OrganizationPhotoDto> Photos { get; set; }
    public List<MemberDto> OrganizationUsers { get; set; }
    public List<ContactDto> Contacts { get; set; } = new();

    public List<OrganizationRequestDto> Requests { get; set; }

    public DateTime Created { get; set; }
}