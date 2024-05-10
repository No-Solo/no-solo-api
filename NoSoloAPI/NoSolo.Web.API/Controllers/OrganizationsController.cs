using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Organizations;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Organizations.Organizations;
using NoSolo.Core.Enums;
using NoSolo.Core.Specification.Organization.Organization;
using NoSolo.Web.API.Extensions;

namespace NoSolo.Web.API.Controllers;

[Authorize]
[Route("api/organizations")]
[ExcludeFromCodeCoverage]
public class OrganizationsController(IOrganizationService organizationService) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<Pagination<OrganizationDto>>> GetAllOrganizations(
        [FromQuery] OrganizationParams organizationParams)
    {
        return await organizationService.Get(organizationParams);
    }

    [AllowAnonymous]
    [HttpGet("{organizationId:guid}")]
    public async Task<OrganizationDto> GetOrganization(Guid organizationId)
    {
        return await organizationService.Get(organizationId);
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<Pagination<OrganizationDto>> GetMy()
    {
        return await organizationService.GetMy(User.GetUserId());
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<OrganizationDto>> CreateOrganization(
        [FromBody] NewOrganizationDto organizationDto)
    {
        return await organizationService.Create(organizationDto, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("delete/{organizationId:guid}")]
    public async Task DeleteOrganization(Guid organizationId)
    {
        await organizationService.Delete(organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<ActionResult<OrganizationDto>> UpdateOrganization(
        [FromBody] UpdateOrganizationDto organizationDto)
    {
        return await organizationService.Update(organizationDto, User.GetEmail());
    }

    [Authorize]
    [HttpPost("add-to/{organizationId:guid}/{targetEmail}")]
    public async Task AddMemberToOrganization(Guid organizationId, string targetEmail)
    {
        await organizationService.AddMember(organizationId, User.GetEmail(), targetEmail);
    }

    [Authorize]
    [HttpPost("remove-from/{organizationId:guid}/{targetEmail}")]
    public async Task RemoveMemberFromOrganization(Guid organizationId, string targetEmail)
    {
        await organizationService.RemoveMember(organizationId, User.GetEmail(), targetEmail);
    }

    [Authorize]
    [HttpPost("upgrade-role/{organizationId:guid}/{targetEmail}/{role}")]
    public async Task UpgradeRoleForMember(Guid organizationId, string targetEmail, RoleEnum role)
    {
        await organizationService.UpdateRoleForMember(organizationId, User.GetEmail(), targetEmail, role);
    }
}