using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Requests;
using NoSolo.Contracts.Dtos.Organizations.Requests;
using NoSolo.Contracts.Dtos.Users.Requests;
using NoSolo.Core.Enums;

namespace NoSolo.Web.API.Controllers;

[Authorize]
[Route("api/requests")]
public class RequestsController : BaseApiController
{
    private readonly IUserRequestService _userRequestService;
    private readonly IOrganizationRequestService _organizationRequestService;

    public RequestsController(IUserRequestService userRequestService, IOrganizationRequestService organizationRequestService)
    {
        _userRequestService = userRequestService;
        _organizationRequestService = organizationRequestService;
    }

    #region Organization
    
    [HttpPost("organization/{organizationGuid:guid}/{userOfferGuid:guid}")]
    public async Task<OrganizationRequestDto> CreateOrganizationRequest(Guid organizationGuid, Guid userOfferGuid)
    {
        return await _organizationRequestService.Send(organizationGuid, userOfferGuid);
    }
    
    [HttpGet("organization/request/{organizationRequestGuid}")]
    public async Task<OrganizationRequestDto> GetOrganizationRequest(Guid organizationRequestGuid)
    {
        return await _organizationRequestService.Get(organizationRequestGuid);
    }
    
    [HttpGet("organization/{organizationGuid:guid}")]
    public async Task<IReadOnlyList<OrganizationRequestDto>> GetOrganizationRequests(Guid organizationGuid)
    {
        return await _organizationRequestService.GetByOrganization(organizationGuid);
    }
    
    [HttpGet("organization/offer/{organizationOfferGuid:guid}")]
    public async Task<IReadOnlyList<UserRequestDto>> GetUserRequestByOrganizationOffer(Guid organizationOfferGuid)
    {
        return await _userRequestService.GetByOrganizationOffer(organizationOfferGuid);
    }
    
    [HttpGet("organization/{organizationGuid:guid}/{userOfferGuid:guid}")]
    public async Task<OrganizationRequestDto> GetOrganizationRequest(Guid organizationGuid, Guid userOfferGuid)
    {
        return await _organizationRequestService.Get(organizationGuid, userOfferGuid);
    }

    [HttpPut("organization/{organizationGuid:guid}/{userOfferGuid:guid}/{newStatus}")]
    public async Task<StatusEnum> UpdateOrganizationRequest(Guid organizationGuid, Guid userOfferGuid, StatusEnum newStatus)
    {
        return await _organizationRequestService.UpdateStatus(organizationGuid, userOfferGuid, newStatus);
    }
    
    [HttpDelete("organization/{organizationGuid:guid}/{userOfferGuid:guid}")]
    public async Task DeleteOrganizationRequest(Guid organizationGuid, Guid userOfferGuid)
    {
        await _organizationRequestService.Delete(organizationGuid, userOfferGuid);
    }
    
    #endregion

    #region User
    
    [HttpPost("user/{userGuid:guid}/{organizationOfferGuid:guid}")]
    public async Task<UserRequestDto> CreateUserRequest(Guid userGuid, Guid organizationOfferGuid)
    {
        return await _userRequestService.Send(userGuid, organizationOfferGuid);
    }
    
    [HttpGet("user/request/{organizationRequestGuid}")]
    public async Task<UserRequestDto> GetUserRequest(Guid userRequestGuid)
    {
        return await _userRequestService.Get(userRequestGuid);
    }
    
    [HttpGet("user/offer/{userOfferGuid:guid}")]
    public async Task<IReadOnlyList<OrganizationRequestDto>> GetOrganizationRequestByOrganizationOffer(Guid userOfferGuid)
    {
        return await _organizationRequestService.GetByUserOffer(userOfferGuid);
    }
    
    [HttpGet("user/{userGuid:guid}")]
    public async Task<IReadOnlyList<UserRequestDto>> GetUserRequests(Guid userGuid)
    {
        return await _userRequestService.GetByUser(userGuid);
    }
    
    [HttpGet("user/{userGuid:guid}/{organizationOfferGuid:guid}")]
    public async Task<UserRequestDto> GetUserRequest(Guid userGuid, Guid organizationOfferGuid)
    {
        return await _userRequestService.Get(userGuid, organizationOfferGuid);
    }
    
    [HttpPut("user/{userGuid:guid}/{organizationOfferGuid:guid}/{newStatus}")]
    public async Task<StatusEnum> UpdateUserRequest(Guid userGuid, Guid organizationOfferGuid, StatusEnum newStatus)
    {
        return await _userRequestService.UpdateStatus(userGuid, organizationOfferGuid, newStatus);
    }

    [HttpDelete("user/{userGuid:guid}/{organizationOfferGuid:guid}")]
    public async Task DeleteUserRequest(Guid userGuid, Guid organizationOfferGuid)
    {
        await _userRequestService.Delete(userGuid, organizationOfferGuid);
    }
    
    #endregion
}