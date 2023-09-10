using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = "HasProfile")]
public class UserContactController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserContactController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("contacts", Name = "GetUserContacts")]
    public async Task<ActionResult<IReadOnlyList<ContactDto>>> GetUserProfileContacts()
    {
        var userProfile = await
            _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithContactsIncludeAsync(User.GetUsername());

        if (userProfile.Contacts == null)
            return NotFound(new ApiResponse(404, "The contacts not found"));
        
        var userProfileToReturn = _mapper.Map<UserProfileDto>(userProfile);
        
        return Ok(_mapper.Map<IReadOnlyList<ContactDto>>(userProfileToReturn.Contacts));
    }
    
    // [HttpGet("my/{id:guid}")]
    // public async Task<ActionResult<ContactDto>> GetMyUserProfileContactByGuid(Guid id)
    // {
    //     
    // }
    //
    // [HttpGet("{id:guid}")]
    // public async Task<ActionResult<ContactDto>> GetUserProfileContactByGuid(Guid id)
    // {
    //     var userProfile = await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithContactsIncludeAsync(id);
    // }
    
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
}