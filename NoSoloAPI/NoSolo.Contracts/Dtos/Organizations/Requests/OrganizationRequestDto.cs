using NoSolo.Contracts.Dtos.Base;
using NoSolo.Core.Enums;

namespace NoSolo.Contracts.Dtos.Organizations.Requests;

public class OrganizationRequestDto : BaseDto
{
    public Guid OrganizationGuid { get; set; }
    public StatusEnum Status { get; set; }
    public Guid UserOfferGuid { get; set; }
}