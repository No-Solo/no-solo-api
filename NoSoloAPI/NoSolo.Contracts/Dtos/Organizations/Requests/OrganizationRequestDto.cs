using NoSolo.Contracts.Dtos.Base;
using NoSolo.Core.Enums;

namespace NoSolo.Contracts.Dtos.Organizations.Requests;

public record OrganizationRequestDto : BaseDto<Guid>
{
    public required Guid OrganizationGuid { get; set; }
    public required StatusEnum Status { get; set; }
    public required Guid UserOfferGuid { get; set; }
}