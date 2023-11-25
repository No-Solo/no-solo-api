using NoSolo.Web.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Organizations;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Organization;
using NoSolo.Contracts.Dtos.Organization.Update;
using NoSolo.Contracts.Dtos.Organizations.Organizations;
using NoSolo.Core.Enums;
using NoSolo.Core.Specification.Organization.Organization;

namespace NoSolo.Web.API.Controllers;

[Authorize]
[Route("api/organizations")]
public class OrganizationsController : BaseApiController
{
    private readonly IOrganizaitonService _organizaitonService;

    public OrganizationsController(IOrganizaitonService organizaitonService)
    {
        _organizaitonService = organizaitonService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<Pagination<OrganizationDto>>> GetAllOrganizations(
        [FromQuery] OrganizationParams organizationParams)
    {
        return await _organizaitonService.Get(organizationParams);
    }

    [AllowAnonymous]
    [HttpGet("{organizationId:guid}")]
    public async Task<OrganizationDto> GetOrganization(Guid organizationId)
    {
        return await _organizaitonService.Get(organizationId);
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<OrganizationDto>> CreateOrganization(
        [FromBody] NewOrganizationDto organizationDto)
    {
        return await _organizaitonService.Create(organizationDto, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("delete/{organizationId:guid}")]
    public async Task DeleteOrganization(Guid organizationId)
    {
        await _organizaitonService.Delete(organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<ActionResult<OrganizationDto>> UpdateOrganization(
        [FromBody] UpdateOrganizationDto organizationDto)
    {
        return await _organizaitonService.Update(organizationDto, User.GetEmail());
    }

    [Authorize]
    [HttpPost("add-to/{organizationId:guid}/{targetEmail}")]
    public async Task AddMemberToOrganization(Guid organizationId, string targetEmail)
    {
        await _organizaitonService.AddMember(organizationId, User.GetEmail(), targetEmail);
    }

    [Authorize]
    [HttpPost("remove-from/{organizationId:guid}/{targetEmail}")]
    public async Task RemoveMemberFromOrganization(Guid organizationId, string targetEmail)
    {
        await _organizaitonService.RemoveMember(organizationId, User.GetEmail(), targetEmail);
    }

    [Authorize]
    [HttpPost("upgrade-role/{organizationId:guid}/{targetEmail}/{role}")]
    public async Task UpgradeRoleForMember(Guid organizationId, string targetEmail, RoleEnum role)
    {
        await _organizaitonService.UpdateRoleForMember(organizationId, User.GetEmail(), targetEmail, role);
    }
    
}