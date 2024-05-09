using AutoMapper;
using NoSolo.Abstractions.Repositories.Requests;
using NoSolo.Abstractions.Services.Requests;
using NoSolo.Contracts.Dtos.Organizations.Requests;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Infrastructure.Services.Requests;

public class OrganizationRequestService(IOrganizationRequestRepository organizationRequestRepository, IMapper mapper)
    : IOrganizationRequestService
{
    public async Task<OrganizationRequestDto> Send(Guid organizationGuid, Guid userOfferGuid)
    {
        var request = new RequestEntity<OrganizationEntity, UserOfferEntity>()
        {
            TEntityId = organizationGuid,
            Status = StatusEnum.Waiting,
            UEntityId = userOfferGuid
        };

        organizationRequestRepository.Add(request);
        
        return mapper.Map<OrganizationRequestDto>(request);
    }

    public async Task<OrganizationRequestDto> Get(Guid organizationGuid, Guid userOfferGuid)
    {
        return mapper.Map<OrganizationRequestDto>(await GetRequest(organizationGuid, userOfferGuid));
    }

    public async Task<OrganizationRequestDto> Get(Guid organizationRequestGuid)
    {
        return mapper.Map<OrganizationRequestDto>(
            await organizationRequestRepository.GetRequest(organizationRequestGuid));
    }

    public async Task<IReadOnlyList<OrganizationRequestDto>> GetByOrganization(Guid organizationGuid)
    {
        return mapper.Map<IReadOnlyList<OrganizationRequestDto>>(await organizationRequestRepository.GetByOrganization(organizationGuid));
    }

    public async Task<IReadOnlyList<OrganizationRequestDto>> GetByUserOffer(Guid userOfferGuid)
    {
        return mapper.Map<IReadOnlyList<OrganizationRequestDto>>(await organizationRequestRepository.GetByUserOffer(userOfferGuid));
    }

    public async Task<StatusEnum> GetStatus(Guid organizationGuid, Guid userOfferGuid)
    {
        var request = await Get(organizationGuid, userOfferGuid);

        return request.Status;
    }

    public async Task<StatusEnum> UpdateStatus(Guid organizationGuid, Guid userOfferGuid, StatusEnum newStatus)
    {
        var request = await Get(organizationGuid, userOfferGuid);

        request.Status = newStatus;
        
        organizationRequestRepository.Save();

        return newStatus;
    }

    public async Task Delete(Guid organizationGuid, Guid userOfferGuid)
    {
        var request = await GetRequest(organizationGuid, userOfferGuid);
        
        organizationRequestRepository.Delete(request);
        organizationRequestRepository.Save();
    }

    private async Task<RequestEntity<OrganizationEntity, UserOfferEntity>> GetRequest(Guid organizationGuid, Guid userOfferGuid)
    {
        return await organizationRequestRepository.Get(organizationGuid, userOfferGuid);
    }
}