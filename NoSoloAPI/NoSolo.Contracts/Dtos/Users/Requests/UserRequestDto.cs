using NoSolo.Contracts.Dtos.Base;
using NoSolo.Core.Enums;

namespace NoSolo.Contracts.Dtos.Users.Requests;

public class UserRequestDto : BaseDto
{
    public Guid UserGuid { get; set; }
    public StatusEnum Status { get; set; }
    public Guid OrganizationOfferGuid { get; set; }
}