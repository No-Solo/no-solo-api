using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
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

        return BadRequest(new ApiResponse(400, "Failed to create organization"));
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