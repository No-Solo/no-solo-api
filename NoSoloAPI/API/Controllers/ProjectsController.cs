using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Specification.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = "HasProfile")]
public class ProjectsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProjectsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectDto>> GetProjectByGuid(Guid id)
    {
        var project = await _unitOfWork.Repository<Project>().GetByGuidAsync(id);

        if (project == null)
            return NotFound(new ApiResponse(404, "The project not found"));

        return Ok(_mapper.Map<ProjectDto>(project));
    }

    [HttpGet("all")]
    public async Task<ActionResult<Pagination<ProjectDto>>> GetAllProjects([FromQuery] ProjectParams projectParams)
    {
        return Ok(await GetProjectsBySpecificationParams(projectParams));
    }

    [HttpPost("{id:guid}/create")]
    public async Task<ActionResult<ProjectDto>> CreateProject(Guid id, [FromBody] CreateProjectDto createProjectDto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithMembersIncludeAsync(User.GetUsername());

        var organization = await _unitOfWork.OrganizationRepository.GetOrganizationWithProjectIncludeByGuid(id);

        if (organization.Project != null)
            return BadRequest(new ApiResponse(400, "The project is exist"));

        var member = user.OrganizationUsers.SingleOrDefault(x => x.OrganizationId == organization.Id && x.UserId == user.Id);

        if (member == null)
            return NotFound(new ApiResponse(404, "The member's instance not found"));
        
        if (member.Role != RoleEnum.Administrator || member.Role != RoleEnum.Owner)
            return BadRequest(new ApiResponse(400, "You don't have permissions"));

        createProjectDto.Name ??= organization.Name;

        var project = new Project
        {
            Name = createProjectDto.Name,
            Description = createProjectDto.Description
        };

        organization.Project = project;

        if (await _unitOfWork.Complete())
            return Ok(_mapper.Map<ProjectDto>(project));
        
        return BadRequest(new ApiResponse(400, "Failed to create organization's project"));
    }

    [HttpPut("{id:guid}/update")]
    public async Task<ActionResult<ProjectDto>> UpdateProject(Guid id, [FromBody] ProjectDto projectDto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithMembersIncludeAsync(User.GetUsername());
        
        var organization = await _unitOfWork.OrganizationRepository.GetOrganizationWithProjectIncludeByGuid(id);

        if (organization.Project != null)
            return BadRequest(new ApiResponse(400, "The project is exist"));

        var member = user.OrganizationUsers.SingleOrDefault(x => x.OrganizationId == organization.Id && x.UserId == user.Id);

        if (member == null)
            return NotFound(new ApiResponse(404, "The member's instance not found"));
        
        if (member.Role != RoleEnum.Owner || member.Role != RoleEnum.Administrator || member.Role != RoleEnum.Moderator)
            return BadRequest(new ApiResponse(400, "You don't have permissions"));

        var project = await _unitOfWork.Repository<Project>().GetByGuidAsync(id);

        _mapper.Map(projectDto, project);
        
        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to delete project"));
    }

    [HttpDelete("{id:guid}/delete")]
    public async Task<ActionResult> DeleteProject(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithMembersIncludeAsync(User.GetUsername());
        
        var organization = await _unitOfWork.OrganizationRepository.GetOrganizationWithProjectIncludeByGuid(id);

        if (organization.Project != null)
            return BadRequest(new ApiResponse(400, "The project is exist"));

        var member = user.OrganizationUsers.SingleOrDefault(x => x.OrganizationId == organization.Id && x.UserId == user.Id);

        if (member == null)
            return NotFound(new ApiResponse(404, "The member's instance not found"));
        
        if (member.Role != RoleEnum.Owner)
            return BadRequest(new ApiResponse(400, "You don't have permissions"));

        organization.Project = null;

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to delete project"));
    }

    private async Task<Pagination<ProjectDto>> GetProjectsBySpecificationParams(ProjectParams projectParams)
    {
        var spec = new ProjectWithSpecificationParams(projectParams);
        
        var countSpec = new ProjectWithFiltersForCountSpecification(projectParams);

        var totalItems = await _unitOfWork.Repository<Project>().CountAsync(countSpec);
        
        var projects = await _unitOfWork.Repository<Project>().ListAsync(spec);
        
        var data = _mapper
            .Map<IReadOnlyList<Project>, IReadOnlyList<ProjectDto>>(projects);

        return new Pagination<ProjectDto>(projectParams.PageNumber, projectParams.PageSize, totalItems, data);
    }
}