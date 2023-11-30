using NoSolo.Contracts.Dtos.Organizations.Requests;
using NoSolo.Core.Enums;


namespace NoSolo.Abstractions.Services.Requests;

public interface IOrganizationRequestService
{
    Task<OrganizationRequestDto> Send(Guid organizationGuid, Guid userOfferGuid);
    Task<OrganizationRequestDto> Get(Guid organizationGuid, Guid userOfferGuid);
    Task<OrganizationRequestDto> Get(Guid organizationRequestGuid);
    Task<IReadOnlyList<OrganizationRequestDto>> GetByOrganization(Guid organizationGuid);
    Task<IReadOnlyList<OrganizationRequestDto>> GetByUserOffer(Guid userOfferGuid);
    Task<StatusEnum> GetStatus(Guid organizationGuid, Guid userOfferGuid);
    Task<StatusEnum> UpdateStatus(Guid organizationGuid, Guid userOfferGuid, StatusEnum newStatus);
    Task Delete(Guid organizationGuid, Guid userOfferGuid);
}