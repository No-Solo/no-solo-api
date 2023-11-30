using NoSolo.Contracts.Dtos.Users.Requests;
using NoSolo.Core.Enums;

namespace NoSolo.Abstractions.Services.Requests;

public interface IUserRequestService
{
    Task<UserRequestDto> Send(Guid userGuid, Guid organizationOfferGuid);
    Task<UserRequestDto> Get(Guid userGuid, Guid organizationOfferGuid);
    Task<UserRequestDto> Get(Guid userRequestGuid);
    Task<IReadOnlyList<UserRequestDto>> GetByUser(Guid userGuid);
    Task<IReadOnlyList<UserRequestDto>> GetByOrganizationOffer(Guid organizationOffer);
    Task<StatusEnum> GetStatus(Guid userGuid, Guid organizationOfferGuid);
    Task<StatusEnum> UpdateStatus(Guid userGuid, Guid organizationOfferGuid, StatusEnum newStatus);
    Task Delete(Guid userGuid, Guid organizationOfferGuid);
}