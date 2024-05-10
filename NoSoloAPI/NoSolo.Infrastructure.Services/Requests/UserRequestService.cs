using AutoMapper;
using NoSolo.Abstractions.Repositories.Requests;
using NoSolo.Abstractions.Services.Requests;
using NoSolo.Contracts.Dtos.Users.Requests;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Infrastructure.Services.Requests;

public class UserRequestService(IUserRequestRepository userRequestRepository, IMapper mapper)
    : IUserRequestService
{
    public async Task<UserRequestDto> Send(Guid userGuid, Guid organizationOfferGuid)
    {
        var request = new RequestEntity<UserEntity, OrganizationOfferEntity>()
        {
            TEntityId = userGuid,
            Status = StatusEnum.Waiting,
            UEntityId = organizationOfferGuid
        };

        userRequestRepository.Add(request);
        
        return mapper.Map<UserRequestDto>(request);
    }

    public async Task<UserRequestDto> Get(Guid userGuid, Guid organizationOfferGuid)
    {
        return mapper.Map<UserRequestDto>(await userRequestRepository.GetByOrganizationOffer(userGuid));
    }

    public async Task<UserRequestDto> Get(Guid userRequestGuid)
    {
        return mapper.Map<UserRequestDto>(await userRequestRepository.GetRequest(userRequestGuid));
    }

    public async Task<IReadOnlyList<UserRequestDto>> GetByUser(Guid userGuid)
    {
        return mapper.Map<IReadOnlyList<UserRequestDto>>(await userRequestRepository.GetByUser(userGuid));
    }

    public async Task<IReadOnlyList<UserRequestDto>> GetByOrganizationOffer(Guid organizationOffer)
    {
        return mapper.Map<IReadOnlyList<UserRequestDto>>(await userRequestRepository.GetByOrganizationOffer(organizationOffer));

    }

    public async Task<StatusEnum> GetStatus(Guid userGuid, Guid organizationOfferGuid)
    {
        var request = await Get(userGuid, organizationOfferGuid);

        return request.Status;
    }

    public async Task<StatusEnum> UpdateStatus(Guid userGuid, Guid organizationOfferGuid, StatusEnum newStatus)
    {
        var request = await Get(userGuid, organizationOfferGuid);

        request.Status = newStatus;
        
        userRequestRepository.Save();

        return newStatus;
    }

    public async Task Delete(Guid userGuid, Guid organizationOfferGuid)
    {
        var request = await GetRequest(userGuid, organizationOfferGuid);
        
        userRequestRepository.Delete(request);
        userRequestRepository.Save();
    }
    
    private async Task<RequestEntity<UserEntity, OrganizationOfferEntity>> GetRequest(Guid userGuid, Guid organizationOfferGuid)
    {
        return await userRequestRepository.Get(userGuid, organizationOfferGuid);
    }
}