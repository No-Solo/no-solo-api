using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification.UserContact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = "HasProfile")]
public class UserContactsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserContactsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("my/contacts/with-params", Name = "GetUserContactsWithParams")]
    public async Task<ActionResult<Pagination<ContactDto>>> GetUserProfileContacts([FromQuery] UserContactParams userContactParams)
    {
        var userProfile = await
            _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithContactsIncludeAsync(User.GetUsername());

        userContactParams.UserProfileId = userProfile.Id;
        
        return Ok(await GetUserContactsBySpecificationParams(userContactParams));
    }
    
    [HttpGet("my/contacts", Name = "GetUserContacts")]
    public async Task<ActionResult<IReadOnlyList<ContactDto>>> GetUserProfileContacts()
    {
        var userProfile = await
            _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithContactsIncludeAsync(User.GetUsername());

        if (userProfile.Contacts == null)
            return NotFound(new ApiResponse(404, "The contacts not found"));
        
        return Ok(_mapper.Map<IReadOnlyList<ContactDto>>(userProfile.Contacts));
    }
    
    [HttpGet("my/{id:guid}")]
    public async Task<ActionResult<ContactDto>> GetMyUserProfileContactByGuid(Guid id)
    {
        var userProfile = await
            _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithContactsIncludeAsync(User.GetUsername());

        var contact = userProfile.Contacts.SingleOrDefault(x => x.Id == id);

        if (contact == null)
            return NotFound(new ApiResponse(404, "The contact not found"));

        return Ok(_mapper.Map<ContactDto>(contact));
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdateUserProfileContact(ContactDto contactDto)
    {
        var userProfile = await
            _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithContactsIncludeAsync(User.GetUsername());

        var userProfileWithContact = await _unitOfWork.UserProfileRepository.GetUserProfileByContactGuid(contactDto.Id);

        if (userProfile.Id != userProfileWithContact.Id)
            return NotFound(new ApiResponse(404, "The contact not found"));

        var contact = await _unitOfWork.Repository<Contact<UserProfile>>().GetByGuidAsync(contactDto.Id);

        contact.Text = contactDto.Text;
        contact.Type = contactDto.Type;
        contact.Url = contactDto.Url;

        if (await _unitOfWork.Complete())
            return Ok();
        
        return BadRequest(new ApiResponse(400, "Failed to update the contact"));
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddContactToUserProfile(CreateContactDto contactDto)
    {
        var userProfile = await
            _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithContactsIncludeAsync(User.GetUsername());

        var contact = new Contact<UserProfile>
        {
            Type = contactDto.Type,
            Text = contactDto.Text,
            Url = contactDto.Url
        };
        
        userProfile.Contacts.Add(contact);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to add contact to your profile"));
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<ActionResult> DeleteContactFromUserProfile(Guid id)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository
                .GetUserProfileByUsernameWithContactsIncludeAsync(User.GetUsername());

        var userProfileWithContact = await _unitOfWork.UserProfileRepository.GetUserProfileByContactGuid(id);
        
        if (userProfile.Id != userProfileWithContact.Id)
            return NotFound(new ApiResponse(404, "The contact not found"));

        userProfile.Contacts.Remove(userProfile.Contacts.SingleOrDefault(x => x.Id == id));

        if (await _unitOfWork.Complete())
            return Ok();
        
        return BadRequest(new ApiResponse(400, "Failed to delete contact from your profile"));
    }
    
    private async Task<Pagination<ContactDto>> GetUserContactsBySpecificationParams(UserContactParams userContactParams)
    {
        var spec = new UserContactWithSpecificationParams(userContactParams);
        
        var countSpec = new UserContactWithFiltersForCountSpecification(userContactParams);

        var totalItems = await _unitOfWork.Repository<Contact<UserProfile>>().CountAsync(countSpec);
        
        var userOffers = await _unitOfWork.Repository<Contact<UserProfile>>().ListAsync(spec);
        
        var data = _mapper
            .Map<IReadOnlyList<Contact<UserProfile>>, IReadOnlyList<ContactDto>>(userOffers);

        return new Pagination<ContactDto>(userContactParams.PageNumber, userContactParams.PageSize, totalItems, data);
    }
}