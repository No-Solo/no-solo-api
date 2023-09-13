using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Specification.Organizations;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = "HasProfile")]
public class OrganizationsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly MemberService _memberService;

    public OrganizationsController(IUnitOfWork unitOfWork, IMapper mapper, MemberService memberService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _memberService = memberService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<Pagination<OrganizationDto>>> GetAllOrganizations(
        [FromQuery] OrganizationParams organizationParams)
    {
        return Ok(await GetOrganizationsBySpecificationParams(organizationParams));
    }

    [HttpPost("create")]
    public async Task<ActionResult<OrganizationDto>> CreateOrganization(
        [FromBody] CreateOrganizationDto organizationDto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithAllIncludesAsync(User.GetUsername());

        var organization = new Organization
        {
            Name = organizationDto.Name
        };

        var member = new OrganizationUser
        {
            Role = RoleEnum.Owner,
            User = user,
            UserId = user.Id,
            Organization = organization,
            OrganizationId = organization.Id
        };

        organization.OrganizationUsers.Add(member);
        user.OrganizationUsers.Add(member);

        if (await _unitOfWork.Complete())
            return Ok(_mapper.Map<OrganizationDto>(organization));

        return BadRequest(new ApiResponse(400, "Failed to create the organization"));
    }

    [HttpDelete("delete/{organizationId:guid}")]
    public async Task<ActionResult> DeleteOrganization(Guid organizationId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithMembersIncludeByGuid(organizationId);

        if (organization == null)
            return NotFound(new ApiResponse(404, "The organization is not found"));

        if (!await _memberService.MemberHasRoleAsync(RoleEnum.Owner, organization, User.GetUsername()))
            return BadRequest(new ApiResponse(400, "You don't have access"));

        _unitOfWork.Repository<Organization>().Delete(organization);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to delete the organization"));
    }

    [HttpPut("update")]
    public async Task<ActionResult<OrganizationDto>> UpdateOrganization(
        [FromBody] UpdateOrganizationDto organizationDto)
    {
        var organization = await _unitOfWork.Repository<Organization>().GetByGuidAsync(organizationDto.Id);

        if (organization == null)
            return NotFound(new ApiResponse(404, "The organization is not found"));

        if (!await _memberService.MemberHasRolesAsync(new[] { RoleEnum.Administrator, RoleEnum.Owner }, organization,
                User.GetUsername()))
            return BadRequest(new ApiResponse(400, "You don't have access"));

        _mapper.Map(organizationDto, organization);

        if (await _unitOfWork.Complete())
            return Ok(_mapper.Map<OrganizationDto>(organization));

        return BadRequest(new ApiResponse(400, "Failed to update the organization"));
    }

    [HttpPost("add-to/{organizationId:guid}/{userId:guid}")]
    public async Task<ActionResult> AddMemberToOrganization(Guid organizationId, Guid userId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithMembersIncludeByGuid(organizationId);

        var user = await _unitOfWork.UserRepository.GetUserByGuidWithMembersIncludeAsync(userId);

        if (!await _memberService.MemberHasRolesAsync(
                new[] { RoleEnum.Administrator, RoleEnum.Moderator, RoleEnum.Owner }, organization, User.GetUsername()))
            return BadRequest(new ApiResponse(400, "You don't have access"));

        if (!await _memberService.MemberHasRoleAsync(RoleEnum.None, organization, user))
            return BadRequest(new ApiResponse(400, "The member is already in the organization"));

        var member = new OrganizationUser
        {
            Role = RoleEnum.Member,
            User = user,
            UserId = user.Id,
            Organization = organization,
            OrganizationId = organization.Id
        };

        organization.OrganizationUsers.Add(member);
        user.OrganizationUsers.Add(member);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to add member to the organization"));
    }

    [HttpPost("remove-from/{organizationId:guid}/{userId:guid}")]
    public async Task<ActionResult> RemoveMemberFromOrganization(Guid organizationId, Guid userId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithMembersIncludeByGuid(organizationId);

        var removingUser = await _unitOfWork.UserRepository.GetUserByGuidWithMembersIncludeAsync(userId);

        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithMembersIncludeAsync(User.GetUsername());

        if (!await _memberService.MemberHasRolesAsync(
                new[] { RoleEnum.Administrator, RoleEnum.Moderator, RoleEnum.Owner }, organization, User.GetUsername()))
            return BadRequest(new ApiResponse(400, "You don't have access"));

        if (await _memberService.MemberHasRoleAsync(RoleEnum.None, organization, removingUser))
            return BadRequest(new ApiResponse(400, "The member not in the organization"));

        var removingMember = removingUser.OrganizationUsers.SingleOrDefault(x =>
            x.OrganizationId == organization.Id && x.UserId == removingUser.Id);

        var member =
            user.OrganizationUsers.SingleOrDefault(x => x.OrganizationId == organization.Id && x.UserId == user.Id);

        if (_memberService.More(removingMember, member))
            return BadRequest(new ApiResponse(400, "You don't have access"));

        _unitOfWork.Repository<OrganizationUser>().Delete(removingMember);

        if (await _unitOfWork.Complete())
            return Ok(new ApiResponse(200, "The member successfully deleted from the organization"));

        return BadRequest(new ApiResponse(400, "Failed to add member to the organization"));
    }

    [HttpPost("upgrade-role/{organizationId:guid}/{userId:guid}/{role}")]
    public async Task<ActionResult> UpgradeRoleForMember(Guid organizationId, Guid userId, string role)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithMembersIncludeByGuid(organizationId);

        var targetUser = await _unitOfWork.UserRepository.GetUserByGuidWithMembersIncludeAsync(userId);

        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithMembersIncludeAsync(User.GetUsername());

        if (!await _memberService.MemberHasRolesAsync(
                new[] { RoleEnum.Administrator, RoleEnum.Moderator, RoleEnum.Owner }, organization, user))
            return BadRequest(new ApiResponse(400, "You don't have access"));

        if (await _memberService.MemberHasRoleAsync(RoleEnum.None, organization, targetUser))
            return BadRequest(new ApiResponse(400, "The member not in the organization"));

        

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to upgrade role"));
    }

    private async Task<Pagination<OrganizationDto>> GetOrganizationsBySpecificationParams(
        OrganizationParams organizationParams)
    {
        var spec = new OrganizationWithSpecificationParams(organizationParams);

        var countSpec = new OrganizationWithFiltersForCountSpecification(organizationParams);

        var totalItems = await _unitOfWork.Repository<Organization>().CountAsync(countSpec);

        var organizations = await _unitOfWork.Repository<Organization>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<Organization>, IReadOnlyList<OrganizationDto>>(organizations);

        return new Pagination<OrganizationDto>(organizationParams.PageNumber, organizationParams.PageSize, totalItems,
            data);
    }
}