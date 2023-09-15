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
using Core.Specification.Organization.Organization;
using Core.Specification.Organization.OrganizationContact;
using Core.Specification.Organization.OrganizationOffer;
using Core.Specification.Organization.OrganizationPhotoParams;
using Core.Specification.OrganizationContact;
using Core.Specification.Organizations;
using Core.Specification.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = "HasProfile")]
public class OrganizationsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemberService _memberService;
    private readonly IPhotoService _photoService;

    public OrganizationsController(IUnitOfWork unitOfWork, IMapper mapper, IMemberService memberService, IPhotoService photoService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _memberService = memberService;
        _photoService = photoService;
    }

    #region Organizaiton

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
    
    #endregion

    #region Organization Contacts

    [AllowAnonymous]
    [HttpGet("{organizationId:guid}/contacts")]
    public async Task<ActionResult<IReadOnlyList<ContactDto>>> GetOrganizationContacts(Guid organizationId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithContactsIncludeByGuid(organizationId);

        if (organization.Contacts == null)
            return NotFound(new ApiResponse(404, "Contacts not found"));

        return Ok(_mapper.Map<IReadOnlyList<ContactDto>>(organization.Contacts));
    }

    [AllowAnonymous]
    [HttpGet("{organizationId:guid}/contacts/with-params")]
    public async Task<ActionResult<Pagination<ContactDto>>> GetOrganizationContacts(
        [FromQuery] OrganizationContactParams organizationContactParams, Guid organizationId)
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

        if (organization == null)
            return NotFound(new ApiResponse(400, "The organization not found"));

        if (!await _memberService.MemberHasRolesAsync(new[] { RoleEnum.Administrator, RoleEnum.Owner }, organization,
                User.GetUsername()))
            return BadRequest(new ApiResponse(400, "You don't have access"));

        var contact = organization.Contacts.SingleOrDefault(x => x.Id == contactDto.Id);

        if (contact == null)
            return NotFound(new ApiResponse(404, "The contact not found"));

        _mapper.Map(contactDto, contact);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to update the contact"));
    }

    [HttpPost("{organizationId:guid}/add-contact")]
    public async Task<ActionResult> AddContactToOrganization([FromBody] CreateContactDto contactDto,
        Guid organizationId)
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

    #endregion

    #region Organization Offers

    [AllowAnonymous]
    [HttpGet("{organizationId:guid}/offers/with-params")]
    public async Task<ActionResult<Pagination<OrganizationOfferDto>>> GetOrganizationOffersWithParams(
        Guid organizationId,
        [FromQuery] OrganizationOfferParams organizationOfferParams)
    {
        organizationOfferParams.OrganizationId = organizationId;
        return Ok(await GetOrganizationOffersBySpecificationParams(organizationOfferParams));
    }

    [AllowAnonymous]
    [HttpGet("{organizationId:guid}/offers")]
    public async Task<ActionResult<IReadOnlyList<OrganizationOfferDto>>> GetOrganizationOffers(Guid organizationId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithOffersIncludeByGuid(organizationId);

        if (organization == null)
            return NotFound(new ApiResponse(404, "The organization not found"));
        if (organization.Offers == null)
            return NotFound(new ApiResponse(404, "Offers not found"));

        return Ok(_mapper.Map<IReadOnlyList<OrganizationOfferDto>>(organization.Offers));
    }

    [AllowAnonymous]
    [HttpGet("offers/with-params")]
    public async Task<ActionResult<Pagination<OrganizationOfferDto>>> GetOffersWithParams(
        [FromQuery] OrganizationOfferParams organizationOfferParams)
    {
        return Ok(await GetOrganizationOffersBySpecificationParams(organizationOfferParams));
    }

    [AllowAnonymous]
    [HttpGet("offers")]
    public async Task<ActionResult<IReadOnlyList<OrganizationOfferDto>>> GetOrganizationOffers()
    {
        var offers = await _unitOfWork.Repository<OrganizationOffer>().ListAllAsync();

        return Ok(_mapper.Map<IReadOnlyList<OrganizationOfferDto>>(offers));
    }

    [HttpPost("{organizationId:guid}/create-offer")]
    public async Task<ActionResult> CreateOrganizationOffer(Guid organizationId,
        [FromBody] CreateOrganizationOfferDto organizationOfferDto)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithOffersIncludeByGuid(organizationId);

        if (organization == null)
            return NotFound(new ApiResponse(404, "The organization not found"));

        if (!await _memberService.MemberHasRolesAsync(
                new[] { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organization, User.GetUsername()))
            return BadRequest(new ApiResponse(400, "You don't have access"));

        var organizationOffer = new OrganizationOffer
        {
            Name = organizationOfferDto.Name,
            Description = organizationOfferDto.Description,
            Tags = organizationOfferDto.Tags
        };

        organization.Offers.Add(organizationOffer);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to create offer"));
    }

    [HttpPut("{organizationId:guid}/update-offer")]
    public async Task<ActionResult> UpdateOrganizationOffer(Guid organizationId,
        [FromBody] OrganizationOfferDto organizationOfferDto)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithOffersIncludeByGuid(organizationId);

        if (organization == null)
            return NotFound(new ApiResponse(404, "The organization not found"));

        if (!await _memberService.MemberHasRolesAsync(
                new[] { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organization, User.GetUsername()))
            return BadRequest(new ApiResponse(400, "You don't have access"));

        var offer = organization.Offers.SingleOrDefault(x => x.Id == organizationOfferDto.Id);

        if (offer == null)
            return NotFound(new ApiResponse(404, "The offer not found"));

        _mapper.Map(organizationOfferDto, offer);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to create offer"));
    }

    [HttpDelete("{organizationId:guid}/delete-offer/{offerId:guid}")]
    public async Task<ActionResult> DeleteOffer(Guid organizationId, Guid offerId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithOffersIncludeByGuid(organizationId);

        if (organization == null)
            return NotFound(new ApiResponse(404, "The organization not found"));

        if (!await _memberService.MemberHasRolesAsync(
                new[] { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organization, User.GetUsername()))
            return BadRequest(new ApiResponse(400, "You don't have access"));

        var offer = organization.Offers.SingleOrDefault(x => x.Id == offerId);

        if (offer == null)
            return NotFound(new ApiResponse(404, "The offer not found"));

        _unitOfWork.Repository<OrganizationOffer>().Delete(offer);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to create offer"));
    }

    #endregion

    #region Organization Photo

    [AllowAnonymous]
    [HttpGet("{organizationId:guid}/photo")]
    public async Task<ActionResult<OrganizationPhotoDto>> GetMainOrganizationPhoto(Guid organizationId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithPhotosIncludeByGuid(organizationId);

        var photo = organization.Photos.SingleOrDefault(x => x.IsMain = true);

        if (photo == null)
            return NotFound(new ApiResponse(404, "The organization doesn't have main photo"));

        return Ok(_mapper.Map<OrganizationPhotoDto>(photo));
    }

    [AllowAnonymous]
    [HttpGet("{organizationId:guid}/photos")]
    public async Task<ActionResult<IReadOnlyList<OrganizationPhotoDto>>> GetOrganizationPhotos(Guid organizationId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithPhotosIncludeByGuid(organizationId);

        if (organization.Photos == null)
            return NotFound(new ApiResponse(404, "The organization doesn't have photos"));

        return Ok(_mapper.Map<IReadOnlyList<OrganizationPhotoDto>>(organization.Photos));
    }

    [AllowAnonymous]
    [HttpGet("{organizationId:guid}/photos/with-params")]
    public async Task<ActionResult<IReadOnlyList<OrganizationPhotoDto>>> GetOrganizationPhotosWithParams(
        Guid organizationId, OrganizationPhotoParams organizationPhotoParams)
    {
        organizationPhotoParams.OrganizationId = organizationId;
        return Ok(await GetOrganizationPhotosBySpecificationParams(organizationPhotoParams));
    }

    [HttpPost("{organizationId:guid}/add-photo")]
    public async Task<ActionResult> AddPhotoToOrganization(Guid organizationId, IFormFile file)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithPhotosIncludeByGuid(organizationId);

        if (organization == null)
            return NotFound(new ApiResponse(404, "The organization not found"));
        
        if (!await _memberService.MemberHasRolesAsync(new[] { RoleEnum.Administrator, RoleEnum.Owner }, organization,
                User.GetUsername()))
            return BadRequest(new ApiResponse(400, "You don't have access"));
        
        var result = await _photoService.AddPhotoAsync(file);

        if (result.Error != null)
            return BadRequest(new ApiResponse(400, result.Error.Message));

        var photo = new OrganizationPhoto
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId,
            IsMain = false
        };
        
        if (organization.Photos.Count == 0)
            photo.IsMain = true;

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to add photo to organization"));
    }

    [HttpPut("{organizationId:guid}/set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(Guid organizationId, Guid photoId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithPhotosIncludeByGuid(organizationId);
        
        if (organization == null)
            return NotFound(new ApiResponse(404, "The organization not found"));

        if (!await _memberService.MemberHasRolesAsync(new[] { RoleEnum.Owner }, organization, User.GetUsername()))
            return BadRequest(new ApiResponse(400, "You don't have access"));
        
        var photo = organization.Photos.FirstOrDefault(photo => photo.Id == photoId);

        if (photo == null)
            return NotFound(new ApiResponse(404, "The photo not found"));
        if (photo.IsMain)
            return BadRequest("This is already your main photo");
        
        var currentMainPhoto = organization.Photos.FirstOrDefault(organizationPhoto => organizationPhoto.IsMain);
        
        if (currentMainPhoto != null)
            currentMainPhoto.IsMain = false;

        photo.IsMain = true;

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to set main photo for organization"));
    }

    [HttpDelete("{organizationId:guid}/delete-photo/{photoId}")]
    public async Task<ActionResult> DeletePhoto(Guid organizationId, Guid photoId)
    {
        var organization =
            await _unitOfWork.OrganizationRepository.GetOrganizationWithPhotosIncludeByGuid(organizationId);
        
        if (organization == null)
            return NotFound(new ApiResponse(404, "The organization not found"));
        
        if (await _memberService.MemberHasRolesAsync(new [] { RoleEnum.Owner, RoleEnum.Administrator }, organization, User.GetUsername()))
            return BadRequest(new ApiResponse(400, "You don't have access"));
        
        var photo = organization.Photos.FirstOrDefault(photo => photo.Id == photoId);

        if (photo == null)
            return NotFound(new ApiResponse(404, "The photo not found"));

        if (photo.IsMain)
            return BadRequest("You cannot delete your main photo");

        if (photo.PublicId != null)
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);

            if (result.Error != null)
                return BadRequest(result.Error.Message);
        }

        organization.Photos.Remove(photo);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest("Problem deleting photo");
    }

    #endregion

    #region Project

    [HttpGet("project/{id:guid}")]
    public async Task<ActionResult<ProjectDto>> GetProjectByGuid(Guid id)
    {
        var project = await _unitOfWork.Repository<Project>().GetByGuidAsync(id);

        if (project == null)
            return NotFound(new ApiResponse(404, "The project not found"));

        return Ok(_mapper.Map<ProjectDto>(project));
    }

    [HttpGet("projects")]
    public async Task<ActionResult<Pagination<ProjectDto>>> GetAllProjects([FromQuery] ProjectParams projectParams)
    {
        return Ok(await GetProjectsBySpecificationParams(projectParams));
    }

    [HttpPost("{organizationId:guid}/project/create")]
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

    [HttpPut("{organizationId:guid}/project/update")]
    public async Task<ActionResult<ProjectDto>> UpdateProject(Guid organizationId, [FromBody] ProjectDto projectDto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithMembersIncludeAsync(User.GetUsername());
        
        var organization = await _unitOfWork.OrganizationRepository.GetOrganizationWithProjectIncludeByGuid(organizationId);

        if (organization.Project != null)
            return BadRequest(new ApiResponse(400, "The project is exist"));

        var member = user.OrganizationUsers.SingleOrDefault(x => x.OrganizationId == organization.Id && x.UserId == user.Id);

        if (member == null)
            return NotFound(new ApiResponse(404, "The member's instance not found"));
        
        if (member.Role != RoleEnum.Owner || member.Role != RoleEnum.Administrator || member.Role != RoleEnum.Moderator)
            return BadRequest(new ApiResponse(400, "You don't have permissions"));

        var project = await _unitOfWork.Repository<Project>().GetByGuidAsync(organizationId);

        _mapper.Map(projectDto, project);
        
        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to delete project"));
    }

    [HttpDelete("{organizationId:guid}/project/delete")]
    public async Task<ActionResult> DeleteProject(Guid organizationId)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithMembersIncludeAsync(User.GetUsername());
        
        var organization = await _unitOfWork.OrganizationRepository.GetOrganizationWithProjectIncludeByGuid(organizationId);

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

    #endregion
    
    #region Specification

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

    private async Task<Pagination<ContactDto>> GetOrganizationContactsBySpecificationParams(
        OrganizationContactParams organizationContactParams)
    {
        var spec = new OrganizationContactWithSpecificationParams(organizationContactParams);

        var countSpec = new OrganizationContactWithFiltersForCountSpecification(organizationContactParams);

        var totalItems = await _unitOfWork.Repository<Contact<Organization>>().CountAsync(countSpec);

        var userOffers = await _unitOfWork.Repository<Contact<Organization>>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<Contact<Organization>>, IReadOnlyList<ContactDto>>(userOffers);

        return new Pagination<ContactDto>(organizationContactParams.PageNumber, organizationContactParams.PageSize,
            totalItems, data);
    }

    private async Task<Pagination<OrganizationOfferDto>> GetOrganizationOffersBySpecificationParams(
        OrganizationOfferParams organizationOfferParams)
    {
        var spec = new OrganizationOfferWithSpecificationParams(organizationOfferParams);

        var countSpec = new OrganizationOfferWithFiltersForCountSpecification(organizationOfferParams);

        var totalItems = await _unitOfWork.Repository<OrganizationOffer>().CountAsync(countSpec);

        var userOffers = await _unitOfWork.Repository<OrganizationOffer>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<OrganizationOffer>, IReadOnlyList<OrganizationOfferDto>>(userOffers);

        return new Pagination<OrganizationOfferDto>(organizationOfferParams.PageNumber,
            organizationOfferParams.PageSize, totalItems, data);
    }

    private async Task<Pagination<OrganizationPhotoDto>> GetOrganizationPhotosBySpecificationParams(
        OrganizationPhotoParams organizationPhotoParams)
    {
        var spec = new OrganizationPhotoWithSpecificationParams(organizationPhotoParams);

        var countSpec = new OrganizationPhotoWithPaginationForCountSpecification(organizationPhotoParams);

        var totalItems = await _unitOfWork.Repository<OrganizationPhoto>().CountAsync(countSpec);

        var userOffers = await _unitOfWork.Repository<OrganizationPhoto>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<OrganizationPhoto>, IReadOnlyList<OrganizationPhotoDto>>(userOffers);

        return new Pagination<OrganizationPhotoDto>(organizationPhotoParams.PageNumber,
            organizationPhotoParams.PageSize, totalItems, data);
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
    
    #endregion
}