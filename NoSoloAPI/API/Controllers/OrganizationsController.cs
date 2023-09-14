using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Interfaces.Data;
using Core.Interfaces.Services;
using Core.Specification.OrganizationContact;
using Core.Specification.Organizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = "HasProfile")]
public class OrganizationsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemberService _memberService;

    public OrganizationsController(IUnitOfWork unitOfWork, IMapper mapper, IMemberService memberService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _memberService = memberService;
    }

    [AllowAnonymous]
    [HttpGet("all/with-params")]
    public async Task<ActionResult<Pagination<OrganizationDto>>> GetAllOrganizations(
        [FromQuery] OrganizationParams organizationParams)
    {
        return Ok(await GetOrganizationsBySpecificationParams(organizationParams));
    }

    [AllowAnonymous]
    [HttpGet("{organizationId:guid}")]
    public async Task<ActionResult<OrganizationDto>> GetOrganization(Guid organizationId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithAllIncludesByGuid(organizationId);

        return Ok(_mapper.Map<OrganizationDto>(organization));
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
    
    /// <summary>
    /// Contacts
    /// </summary>
    /// <param name="organizationId"></param>
    /// <returns></returns>
    
    [AllowAnonymous]
    [HttpGet("{organizationId:guid}/contacts/all")]
    public async Task<ActionResult<IReadOnlyList<ContactDto>>> GetOrganizationContacts(Guid organizationId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithContactsIncludeByGuid(organizationId);

        if (organization.Contacts == null)
            return NotFound(new ApiResponse(404, "Contacts not found"));
        
        return Ok(_mapper.Map<IReadOnlyList<ContactDto>>(organization.Contacts));
    }
    
    [AllowAnonymous]
    [HttpGet("{organizationId:guid}/contacts/all/with-params")]
    public async Task<ActionResult<Pagination<ContactDto>>> GetOrganizationContacts([FromQuery] OrganizationContactParams organizationContactParams, Guid organizationId)
    {
        organizationContactParams.OrganizationId = organizationId;

        return Ok(await GetOrganizationContactsBySpecificationParams(organizationContactParams));
    }

    [AllowAnonymous]
    [HttpGet("{organizationId:Guid}/contacts/{contactId:guid}")]
    public async Task<ActionResult<ContactDto>> GetOrganizationContactByGuid(Guid organizationId, Guid contactId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithContactsIncludeByGuid(organizationId);
        
        var contact = organization.Contacts.SingleOrDefault(x => x.Id == contactId);
        
        if (contact == null)
            return NotFound(new ApiResponse(404, "The contact not found"));

        return Ok(_mapper.Map<ContactDto>(contact));
    }

    [HttpPut("{organizationId:guid}/update-contact")]
    public async Task<ActionResult> UpdateUserProfileContact(Guid organizationId, ContactDto contactDto)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithContactsAndMembersIncludesAsync(organizationId);

        var contact = organization.Contacts.SingleOrDefault(x => x.Id == contactDto.Id);
        
        if (contact == null)
            return NotFound(new ApiResponse(404, "The contact not found"));

        _mapper.Map(contactDto, contact);

        if (await _unitOfWork.Complete())
            return Ok();
        
        return BadRequest(new ApiResponse(400, "Failed to update the contact"));
    }

    [HttpPost("{organizationId:guid}/add-contact")]
    public async Task<ActionResult> AddContactToOrganization([FromBody] CreateContactDto contactDto, Guid organizationId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithContactsAndMembersIncludesAsync(organizationId);

        if (!await _memberService.MemberHasRolesAsync(new[] { RoleEnum.Administrator, RoleEnum.Owner }, organization,
                User.GetUsername()))
            return BadRequest(new ApiResponse(400, "You don't have access"));
        
        var contact = new Contact<Organization>
        {
            Type = contactDto.Type,
            Text = contactDto.Text,
            Url = contactDto.Url
        };
        
        organization.Contacts.Add(contact);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to add contact to organization"));
    }

    [HttpDelete("{organizationId:guid}/delete-contact/{contactId:guid}")]
    public async Task<ActionResult> DeleteContactFromUserProfile(Guid organizationId, Guid contactId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithContactsAndMembersIncludesAsync(organizationId);

        if (!await _memberService.MemberHasRolesAsync(new[] { RoleEnum.Administrator, RoleEnum.Owner }, organization,
                User.GetUsername()))
            return BadRequest(new ApiResponse(400, "You don't have access"));

        organization.Contacts.Remove(organization.Contacts.SingleOrDefault(x => x.Id == contactId));

        if (await _unitOfWork.Complete())
            return Ok();
        
        return BadRequest(new ApiResponse(400, "Failed to delete contact from your profile"));
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
    
    private async Task<Pagination<ContactDto>> GetOrganizationContactsBySpecificationParams(OrganizationContactParams organizationContactParams)
    {
        var spec = new OrganizationContactWithSpecificationParams(organizationContactParams);
        
        var countSpec = new OrganizationContactWithFiltersForCountSpecification(organizationContactParams);

        var totalItems = await _unitOfWork.Repository<Contact<Organization>>().CountAsync(countSpec);
        
        var userOffers = await _unitOfWork.Repository<Contact<Organization>>().ListAsync(spec);
        
        var data = _mapper
            .Map<IReadOnlyList<Contact<Organization>>, IReadOnlyList<ContactDto>>(userOffers);

        return new Pagination<ContactDto>(organizationContactParams.PageNumber, organizationContactParams.PageSize, totalItems, data);
    }
}