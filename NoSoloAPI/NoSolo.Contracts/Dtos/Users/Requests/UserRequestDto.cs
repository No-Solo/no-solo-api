using NoSolo.Contracts.Dtos.Base;
using NoSolo.Core.Enums;

namespace NoSolo.Contracts.Dtos.Users.Requests;

public record UserRequestDto : BaseDto<Guid>
{
    public required Guid UserGuid { get; set; }
    public required StatusEnum Status { get; set; }
    public required Guid OrganizationOfferGuid { get; set; }
}