﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Requests;
using NoSolo.Contracts.Dtos.Organizations.Requests;
using NoSolo.Contracts.Dtos.Users.Requests;
using NoSolo.Core.Enums;

namespace NoSolo.Web.API.Controllers;

[Authorize]
[Route("api/requests")]
[ExcludeFromCodeCoverage]
public class RequestsController(
    IUserRequestService userRequestService,
    IOrganizationRequestService organizationRequestService)
    : BaseApiController
{
    #region OrganizationEntity
    
    [HttpPost("organizationEntity/{organizationGuid:guid}/{userOfferGuid:guid}")]
    public async Task<OrganizationRequestDto> CreateOrganizationRequest(Guid organizationGuid, Guid userOfferGuid)
    {
        return await organizationRequestService.Send(organizationGuid, userOfferGuid);
    }
    
    [HttpGet("organizationEntity/request/{organizationRequestGuid}")]
    public async Task<OrganizationRequestDto> GetOrganizationRequest(Guid organizationRequestGuid)
    {
        return await organizationRequestService.Get(organizationRequestGuid);
    }
    
    [HttpGet("organizationEntity/{organizationGuid:guid}")]
    public async Task<IReadOnlyList<OrganizationRequestDto>> GetOrganizationRequests(Guid organizationGuid)
    {
        return await organizationRequestService.GetByOrganization(organizationGuid);
    }
    
    [HttpGet("organizationEntity/offer/{organizationOfferGuid:guid}")]
    public async Task<IReadOnlyList<UserRequestDto>> GetUserRequestByOrganizationOffer(Guid organizationOfferGuid)
    {
        return await userRequestService.GetByOrganizationOffer(organizationOfferGuid);
    }
    
    [HttpGet("organizationEntity/{organizationGuid:guid}/{userOfferGuid:guid}")]
    public async Task<OrganizationRequestDto> GetOrganizationRequest(Guid organizationGuid, Guid userOfferGuid)
    {
        return await organizationRequestService.Get(organizationGuid, userOfferGuid);
    }

    [HttpPut("organizationEntity/{organizationGuid:guid}/{userOfferGuid:guid}/{newStatus}")]
    public async Task<StatusEnum> UpdateOrganizationRequest(Guid organizationGuid, Guid userOfferGuid, StatusEnum newStatus)
    {
        return await organizationRequestService.UpdateStatus(organizationGuid, userOfferGuid, newStatus);
    }
    
    [HttpDelete("organizationEntity/{organizationGuid:guid}/{userOfferGuid:guid}")]
    public async Task DeleteOrganizationRequest(Guid organizationGuid, Guid userOfferGuid)
    {
        await organizationRequestService.Delete(organizationGuid, userOfferGuid);
    }
    
    #endregion

    #region UserEntity
    
    [HttpPost("userEntity/{userGuid:guid}/{organizationOfferGuid:guid}")]
    public async Task<UserRequestDto> CreateUserRequest(Guid userGuid, Guid organizationOfferGuid)
    {
        return await userRequestService.Send(userGuid, organizationOfferGuid);
    }
    
    [HttpGet("userEntity/request/{organizationRequestGuid}")]
    public async Task<UserRequestDto> GetUserRequest(Guid userRequestGuid)
    {
        return await userRequestService.Get(userRequestGuid);
    }
    
    [HttpGet("userEntity/offer/{userOfferGuid:guid}")]
    public async Task<IReadOnlyList<OrganizationRequestDto>> GetOrganizationRequestByOrganizationOffer(Guid userOfferGuid)
    {
        return await organizationRequestService.GetByUserOffer(userOfferGuid);
    }
    
    [HttpGet("userEntity/{userGuid:guid}")]
    public async Task<IReadOnlyList<UserRequestDto>> GetUserRequests(Guid userGuid)
    {
        return await userRequestService.GetByUser(userGuid);
    }
    
    [HttpGet("userEntity/{userGuid:guid}/{organizationOfferGuid:guid}")]
    public async Task<UserRequestDto> GetUserRequest(Guid userGuid, Guid organizationOfferGuid)
    {
        return await userRequestService.Get(userGuid, organizationOfferGuid);
    }
    
    [HttpPut("userEntity/{userGuid:guid}/{organizationOfferGuid:guid}/{newStatus}")]
    public async Task<StatusEnum> UpdateUserRequest(Guid userGuid, Guid organizationOfferGuid, StatusEnum newStatus)
    {
        return await userRequestService.UpdateStatus(userGuid, organizationOfferGuid, newStatus);
    }

    [HttpDelete("userEntity/{userGuid:guid}/{organizationOfferGuid:guid}")]
    public async Task DeleteUserRequest(Guid userGuid, Guid organizationOfferGuid)
    {
        await userRequestService.Delete(userGuid, organizationOfferGuid);
    }
    
    #endregion
}