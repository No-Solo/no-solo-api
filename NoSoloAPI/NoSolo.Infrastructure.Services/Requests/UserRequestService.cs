using AutoMapper;
using NoSolo.Abstractions.Repositories.Requests;
using NoSolo.Abstractions.Services.Requests;
using NoSolo.Contracts.Dtos.Users.Requests;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Infrastructure.Services.Requests;

public class UserRequestService : IUserRequestService
{
    private readonly IUserRequestRepository _userRequestRepository;
    private readonly IMapper _mapper;

    public UserRequestService(IUserRequestRepository userRequestRepository, IMapper mapper)
    {
        _userRequestRepository = userRequestRepository;
        _mapper = mapper;
    }
    
    public async Task<UserRequestDto> Send(Guid userGuid, Guid organizationOfferGuid)
    {
        var request = new Request<User, OrganizationOffer>()
        {
            TEntityId = userGuid,
            Status = StatusEnum.Waiting,
            UEntityId = organizationOfferGuid
        };

        _userRequestRepository.Add(request);
        
        return _mapper.Map<UserRequestDto>(request);
    }

    public async Task<UserRequestDto> Get(Guid userGuid, Guid organizationOfferGuid)
    {
        return _mapper.Map<UserRequestDto>(await _userRequestRepository.GetByOrganizationOffer(userGuid));
    }

    public async Task<UserRequestDto> Get(Guid userRequestGuid)
    {
        return _mapper.Map<UserRequestDto>(await _userRequestRepository.GetRequest(userRequestGuid));
    }

    public async Task<IReadOnlyList<UserRequestDto>> GetByUser(Guid userGuid)
    {
        return _mapper.Map<IReadOnlyList<UserRequestDto>>(await _userRequestRepository.GetByUser(userGuid));
    }

    public async Task<IReadOnlyList<UserRequestDto>> GetByOrganizationOffer(Guid organizationOffer)
    {
        return _mapper.Map<IReadOnlyList<UserRequestDto>>(await _userRequestRepository.GetByOrganizationOffer(organizationOffer));

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
        
        _userRequestRepository.Save();

        return newStatus;
    }

    public async Task Delete(Guid userGuid, Guid organizationOfferGuid)
    {
        var request = await GetRequest(userGuid, organizationOfferGuid);
        
        _userRequestRepository.Delete(request);
        _userRequestRepository.Save();
    }
    
    private async Task<Request<User, OrganizationOffer>> GetRequest(Guid userGuid, Guid organizationOfferGuid)
    {
        return await _userRequestRepository.Get(userGuid, organizationOfferGuid);
    }
}