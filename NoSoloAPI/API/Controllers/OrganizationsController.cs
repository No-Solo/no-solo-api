using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using CloudinaryDotNet.Actions;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Specification.Organizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = "HasProfile")]
public class OrganizationsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrganizationsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<ActionResult<Pagination<OrganizationDto>>> GetAllOrganizations(
        [FromQuery] OrganizationParams organizationParams)
    {
        return Ok(await GetOrganizationsBySpecificationParams(organizationParams));
    }

    [HttpPost("create")]
    public async Task<ActionResult<OrganizationDto>> CreateOrganization([FromBody] CreateOrganizationDto organizationDto)
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
        var organization = await _unitOfWork.OrganizationRepository.GetOrganizationWithMembersIncludeByGuid(organizationId);

        if (organization == null)
            return NotFound(new ApiResponse(404, "The organization is not found"));
        
        if (!await MemberHasRole(RoleEnum.Owner, organization))
            return BadRequest(new ApiResponse(400, "You don't have access"));
            
        _unitOfWork.Repository<Organization>().Delete(organization);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to delete the organization"));
    }

    [HttpPut("update")]
    public async Task<ActionResult<OrganizationDto>> UpdateOrganization([FromBody] UpdateOrganizationDto organizationDto)
    {
        var organization = await _unitOfWork.Repository<Organization>().GetByGuidAsync(organizationDto.Id);

        if (organization == null)
            return NotFound(new ApiResponse(404, "The organization is not found"));

        if (!await MemberHasRoles(new[] { RoleEnum.Administrator, RoleEnum.Owner }, organization))
            return BadRequest(new ApiResponse(400, "You don't have access"));

        _mapper.Map(organizationDto, organization);
        
        if (await _unitOfWork.Complete())
            return Ok(_mapper.Map<OrganizationDto>(organization));
        
        return BadRequest(new ApiResponse(400, "Failed to update the organization"));
    }

    private async Task<bool> MemberHasRole(RoleEnum role, Organization organization)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithMembersIncludeAsync(User.GetUsername());
        
        var member = user.OrganizationUsers.SingleOrDefault(x => x.OrganizationId == organization.Id && x.UserId == user.Id);

        if (member == null)
            return false;

        if (member.Role == role)
            return true;

        return false;
    }
    
    private async Task<bool> MemberHasRoles(RoleEnum[] roles, Organization organization)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithMembersIncludeAsync(User.GetUsername());
        
        var member = user.OrganizationUsers.SingleOrDefault(x => x.OrganizationId == organization.Id && x.UserId == user.Id);

        if (member == null)
            return false;

        foreach (var role in roles)
        {
            if (member.Role == role)
                return true;   
        }

        return false;
    }

    private async Task<Pagination<OrganizationDto>> GetOrganizationsBySpecificationParams(OrganizationParams organizationParams)
    {
        var spec = new OrganizationWithSpecificationParams(organizationParams);
        
        var countSpec = new OrganizationWithFiltersForCountSpecification(organizationParams);

        var totalItems = await _unitOfWork.Repository<Organization>().CountAsync(countSpec);
        
        var organizations = await _unitOfWork.Repository<Organization>().ListAsync(spec);
        
        var data = _mapper
            .Map<IReadOnlyList<Organization>, IReadOnlyList<OrganizationDto>>(organizations);

        return new Pagination<OrganizationDto>(organizationParams.PageNumber, organizationParams.PageSize, totalItems, data);
    }
}