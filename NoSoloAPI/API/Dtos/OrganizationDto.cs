namespace API.Dtos;

public class OrganizationDto : BaseDto
{
    public string Name { get; set; }
    public string PhotoUrl { get; set; }

    // public List<OrganizationOfferDto> Offers { get; set; } = new();
    // public List<OrganizationPhotoDto> Photos { get; set; } = new();
    public List<OrganizationUserDto> OrganizationUsers { get; set; }
    public List<ContactDto> Contacts { get; set; } = new();
    
    // public List<Request<Organization, UserOffer>> RequestsFromOrganizationToUserOffer { get; set; }

    public DateTime Created { get; set; }
    
    public ProjectDto Project { get; set; }
}