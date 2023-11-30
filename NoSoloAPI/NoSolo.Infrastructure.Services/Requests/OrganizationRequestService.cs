using AutoMapper;
using NoSolo.Abstractions.Repositories.Requests;
using NoSolo.Abstractions.Services.Requests;
using NoSolo.Contracts.Dtos.Organizations.Requests;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Infrastructure.Services.Requests;

public class OrganizationRequestService : IOrganizationRequestService
{
    private readonly IOrganizationRequestRepository _organizationRequestRepository;
    private readonly IMapper _mapper;

    public OrganizationRequestService(IOrganizationRequestRepository organizationRequestRepository, IMapper mapper)
    {
        _organizationRequestRepository = organizationRequestRepository;
        _mapper = mapper;
    }
    
    public async Task<OrganizationRequestDto> Send(Guid organizationGuid, Guid userOfferGuid)
    {
        var request = new Request<Organization, UserOffer>()
        {
            TEntityId = organizationGuid,
            Status = StatusEnum.Waiting,
            UEntityId = userOfferGuid
        };

        _organizationRequestRepository.Add(request);
        
        return _mapper.Map<OrganizationRequestDto>(request);
    }

    public async Task<OrganizationRequestDto> Get(Guid organizationGuid, Guid userOfferGuid)
    {
        return _mapper.Map<OrganizationRequestDto>(await GetRequest(organizationGuid, userOfferGuid));
    }

    public async Task<OrganizationRequestDto> Get(Guid organizationRequestGuid)
    {
        return _mapper.Map<OrganizationRequestDto>(
            await _organizationRequestRepository.GetRequest(organizationRequestGuid));
    }

    public async Task<IReadOnlyList<OrganizationRequestDto>> GetByOrganization(Guid organizationGuid)
    {
        return _mapper.Map<IReadOnlyList<OrganizationRequestDto>>(await _organizationRequestRepository.GetByOrganization(organizationGuid));
    }

    public async Task<IReadOnlyList<OrganizationRequestDto>> GetByUserOffer(Guid userOfferGuid)
    {
        return _mapper.Map<IReadOnlyList<OrganizationRequestDto>>(await _organizationRequestRepository.GetByUserOffer(userOfferGuid));
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
        
        _organizationRequestRepository.Save();

        return newStatus;
    }

    public async Task Delete(Guid organizationGuid, Guid userOfferGuid)
    {
        var request = await GetRequest(organizationGuid, userOfferGuid);
        
        _organizationRequestRepository.Delete(request);
        _organizationRequestRepository.Save();
    }

    private async Task<Request<Organization, UserOffer>> GetRequest(Guid organizationGuid, Guid userOfferGuid)
    {
        return await _organizationRequestRepository.Get(organizationGuid, userOfferGuid);
    }
}