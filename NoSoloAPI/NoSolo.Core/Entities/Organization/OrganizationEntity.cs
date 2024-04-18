using System.ComponentModel.DataAnnotations;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.User;

namespace NoSolo.Core.Entities.Organization;

public class OrganizationEntity : BaseCreatedEntity<Guid>, IBaseForContact
{
    [Required]
    [MaxLength(128)]
    [MinLength(3)]
    public required string Name { get; set; }
    public string? PhotoUrl { get; set; }
    [Required]
    [MaxLength(256)]
    public required string Description { get; set; }
    public int? NumberOfEmployees { get; set; }
    public string? Address { get; set; }
    public string? WebSiteUri { get; set; }

    public List<OrganizationOfferEntity> Offers { get; set; } = new();
    public List<OrganizationPhotoEntity> Photos { get; set; } = new();
    public List<MemberEntity> OrganizationUsers { get; set; } = new();
    public List<ContactEntity<OrganizationEntity>> Contacts { get; set; } = new();

    public List<RequestEntity<OrganizationEntity, UserOfferEntity>> RequestsFromOrganizationToUserOffer { get; set; } = new();
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}