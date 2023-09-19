using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Entities.User;
using Core.Interfaces;
using Core.Interfaces.Data;
using Core.Specification;
using Core.Specification.UserContact;
using Core.Specification.UserOffer;
using Core.Specification.UserTag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;
    private readonly UserManager<User> _userManager;

    public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _photoService = photoService;
        _userManager = userManager;
    }

    #region User

    #region User Tag

    [HttpGet("tags/my", Name = "GetUserProfileTags")]
    public async Task<ActionResult<IReadOnlyList<UserTagDto>>> GetAllUserProfileTags(
        [FromQuery] UserTagParams userTagParams)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithTagsIncludeAsync(User.GetUsername());

        userTagParams.UserProfileId = userProfile.Id;

        return Ok(await GetAllTagsBySpecificationParams(userTagParams));
    }

    [HttpGet("tags/user/{userId:guid}")]
    public async Task<ActionResult<IReadOnlyList<UserTagDto>>> GetAllUserProfileTagsByUserId(Guid userId,
        [FromQuery] UserTagParams userTagParams)
    {
        userTagParams.UserProfileId = userId;

        return Ok(await GetAllTagsBySpecificationParams(userTagParams));
    }
    
    [HttpGet("tags/{tagId:guid}")]
    public async Task<ActionResult<UserTagDto>> GetUserProfileTagByGuid(Guid tagId)
    {
        var userTag = await _unitOfWork.UserTagRepository.GetUserTagByGuid(tagId);

        if (userTag == null)
            return NotFound(new ApiResponse(404, "The user tag not found"));

        return Ok(_mapper.Map<UserTagDto>(userTag));
    }

    [HttpPost("tags/add")]
    public async Task<ActionResult> AddTagToUserProfile([FromBody] CreateUserTagDto userTagDto)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithTagsIncludeAsync(User.GetUsername());

        if (userTagDto != null)
            userProfile.Tags.Add(_mapper.Map<UserTag>(userTagDto));

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Problem adding tag"));
    }

    [HttpPut("tags/update")]
    public async Task<ActionResult> UpdateTag([FromBody] UserTagDto userTagDto)
    {
        var userTag = await _unitOfWork.Repository<UserTag>().GetByGuidAsync(userTagDto.Id);

        if (userTag.UserProfileId != User.GetUserId())
            return NotFound(new ApiResponse(404, "You don't have access"));

        _mapper.Map(userTagDto, userTag);
        
        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to update the tag"));
    }

    [HttpDelete("tags/delete/{tagId:guid}")]
    public async Task<ActionResult> DeleteTagFromUserProfile(Guid tagId)
    {
        var userTag = await _unitOfWork.Repository<UserTag>().GetByGuidAsync(tagId);

        if (userTag.UserProfileId != User.GetUserId())
            return NotFound(new ApiResponse(404, "You don't have access"));

        _unitOfWork.Repository<UserTag>().Delete(userTag);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to delete the tag"));
    }

    [HttpPost("tags/active/{tagId:guid}")]
    public async Task<ActionResult> ChangeActiveTask(Guid tagId)
    {
        var userTag = await _unitOfWork.Repository<UserTag>().GetByGuidAsync(tagId);

        var userId = User.GetUserId();
        
        if (userTag.UserProfileId != User.GetUserId())
            return NotFound(new ApiResponse(404, "You don't have access"));

        userTag.Active = !userTag.Active;

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to change activity of the user tag"));
    }

    #endregion

    #region User Profile
    
    [HttpGet("profiles", Name = "GetAllProfiles")]
    public async Task<ActionResult<IReadOnlyList<UserProfileDto>>> GetAllUserProfiles(
        [FromQuery] UserProfileParams userProfileParams)
    {
        var spec = new UserProfileWithSpecificationParams(userProfileParams);

        var countSpec = new UserProfileWithFiltersForCountSpecification(userProfileParams);

        var totalItems = await _unitOfWork.Repository<UserProfile>().CountAsync(countSpec);

        var userProfiles = await _unitOfWork.Repository<UserProfile>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<UserProfile>, IReadOnlyList<UserProfileDto>>(userProfiles);

        return Ok(new Pagination<UserProfileDto>(userProfileParams.PageNumber, userProfileParams.PageSize, totalItems,
            data));
    }
    
    [HttpGet("profile")]
    public async Task<ActionResult<UserProfileDto>> GetCurrentUserProfile()
    {
        var userProfile = await
            _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithAllIncludesAsync(User.GetUsername());

        if (userProfile == null)
            return NotFound(new ApiResponse(404, "The profile not found"));

        return Ok(_mapper.Map<UserProfileDto>(userProfile));
    }

    [HttpPost("profile/create")]
    public async Task<ActionResult<UserProfileDto>> CreateUserProfile([FromBody] CreateUserProfileDto userProfileDto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithAllIncludesAsync(User.GetUsername());

        if (user.UserProfile != null)
            return BadRequest(new ApiResponse(400, "The user already has a profile"));

        var userProfile = new UserProfile
        {
            FirstName = userProfileDto.FirstName,
            MiddleName = userProfileDto.MiddleName,
            LastName = userProfileDto.LastName,
            About = userProfileDto.About,
            Description = userProfileDto.Description,
            Location = userProfileDto.Location,
            Locale = userProfileDto.Locale,
            Gender = userProfileDto.Gender
        };

        user.UserProfile = userProfile;
        if (await _unitOfWork.Complete())
        {
            await _userManager.AddToRoleAsync(user, "RegisteredUser");
            return Ok(_mapper.Map<UserProfileDto>(user.UserProfile));
        }

        return BadRequest(new ApiResponse(400, "Problem user profile creating"));
    }

    [HttpPut("profile/update")]
    public async Task<ActionResult<UserProfileDto>> UpdateUserProfile([FromBody] UpdateUserProfileDto userProfileDto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithAllIncludesAsync(User.GetUsername());

        if (user.UserProfile == null)
            return NotFound(new ApiResponse(404, "The profile not found"));

        _mapper.Map(userProfileDto, user.UserProfile);

        _unitOfWork.UserRepository.Update(user);

        if (await _unitOfWork.Complete())
            return Ok(_mapper.Map<UserProfileDto>(user.UserProfile));

        return BadRequest(new ApiResponse(400, "Failed to update user"));
    }

    #endregion

    #region User Photo

    [HttpGet("photo")]
    public async Task<ActionResult<UserProfilePhotoDto>> GetUserProfilePhoto()
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithPhotoIncludeAsync(User.GetUsername());

        if (userProfile.Photo == null)
            return NoContent();

        return Ok(_mapper.Map<UserProfilePhotoDto>(userProfile.Photo));
    }

    [HttpPost("photo/add")]
    public async Task<ActionResult<UserProfilePhotoDto>> AddPhotoToUserProfile(IFormFile file)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithPhotoIncludeAsync(User.GetUsername());

        if (user.UserProfile.Photo != null)
            return BadRequest(new ApiResponse(400, "The user already has a photo"));

        var result = await _photoService.AddPhotoAsync(file);

        if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));

        var photo = new UserPhoto()
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        user.UserProfile.Photo = photo;

        if (await _unitOfWork.Complete())
            return CreatedAtRoute("GetUserProfile", _mapper.Map<UserProfilePhotoDto>(photo));

        return BadRequest("Problem adding photo");
    }

    [HttpDelete("photo/delete")]
    public async Task<ActionResult> DeletePhoto()
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithPhotoIncludeAsync(User.GetUsername());

        var photo = userProfile.Photo;

        if (photo == null)
            return NotFound(new ApiResponse(404));

        if (photo.PublicId != null)
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null)
                return BadRequest(new ApiResponse(400, result.Error.Message));
        }

        userProfile.Photo = null;

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest("Failed to delete the photo");
    }

    #endregion

    #region User Contacts

    [HttpGet("contacts/my/with-params", Name = "GetUserContactsWithParams")]
    public async Task<ActionResult<Pagination<ContactDto>>> GetUserProfileContacts(
        [FromQuery] UserContactParams userContactParams)
    {
        var userProfile = await
            _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithContactsIncludeAsync(User.GetUsername());

        userContactParams.UserProfileId = userProfile.Id;

        return Ok(await GetUserContactsBySpecificationParams(userContactParams));
    }

    [HttpGet("contacts/my", Name = "GetUserContacts")]
    public async Task<ActionResult<IReadOnlyList<ContactDto>>> GetUserProfileContacts()
    {
        var userProfile = await
            _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithContactsIncludeAsync(User.GetUsername());

        if (userProfile.Contacts == null)
            return NotFound(new ApiResponse(404, "Contacts not found"));

        return Ok(_mapper.Map<IReadOnlyList<ContactDto>>(userProfile.Contacts));
    }

    [HttpGet("contacts/my/{id:guid}")]
    public async Task<ActionResult<ContactDto>> GetMyUserProfileContactByGuid(Guid id)
    {
        var userProfile = await
            _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithContactsIncludeAsync(User.GetUsername());

        var contact = userProfile.Contacts.SingleOrDefault(x => x.Id == id);

        if (contact == null)
            return NotFound(new ApiResponse(404, "The contact not found"));

        return Ok(_mapper.Map<ContactDto>(contact));
    }

    // [HttpGet("contacts/{userProfileId:guid}/{contactId:guid}")]
    // public async Task<ActionResult<ContactDto>> GetMyUserProfileContactByGuid(Guid userProfileId, Guid contactId)
    // {
    //     var userProfile = await
    //         _unitOfWork.UserProfileRepository.get
    //
    //     var contact = userProfile.Contacts.SingleOrDefault(x => x.Id == id);
    //
    //     if (contact == null)
    //         return NotFound(new ApiResponse(404, "The contact not found"));
    //
    //     return Ok(_mapper.Map<ContactDto>(contact));
    // }

    [HttpPut("contacts/update")]
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

    [HttpPost("contacts/add")]
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

    [HttpDelete("contacts/delete/{offerId:guid}")]
    public async Task<ActionResult> DeleteContactFromUserProfile(Guid offerId)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository
                .GetUserProfileByUsernameWithContactsIncludeAsync(User.GetUsername());

        var contact = userProfile.Contacts.SingleOrDefault(x => x.Id == offerId);

        userProfile.Contacts.Remove(contact);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to delete contact from your profile"));
    }

    #endregion

    #region User Offers

    [AllowAnonymous]
    [HttpGet("offers", Name = "GetAllUserOffers")]
    public async Task<ActionResult<Pagination<UserOfferDto>>> GetAllOffers([FromQuery] UserOfferParams userOfferParams)
    {
        return Ok(await GetUserOffersBySpecificationParams(userOfferParams));
    }

    [AllowAnonymous]
    [HttpGet("offers/{id:guid}")]
    public async Task<ActionResult<IReadOnlyList<UserOfferDto>>> GetOfferByGuid(Guid id)
    {
        return Ok(_mapper.Map<UserOfferDto>(await _unitOfWork.Repository<UserOffer>().GetByGuidAsync(id)));
    }

    [HttpGet("offers/my", Name = "GetMyUserOffers")]
    public async Task<ActionResult<Pagination<UserOfferDto>>> GetUserOffers([FromQuery] UserOfferParams userOfferParams)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithOffersIncludeAsync(User.GetUsername());

        userOfferParams.UserProfileId = userProfile.Id;

        return Ok(await GetUserOffersBySpecificationParams(userOfferParams));
    }

    [HttpPost("offers/add")]
    public async Task<ActionResult> AddUserOffer(CreateUserOfferDto userOfferDto)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithOffersIncludeAsync(User.GetUsername());

        var userOffer = new UserOffer
        {
            Preferences = userOfferDto.Preferences
        };

        userProfile.Offers.Add(userOffer);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to create the offer"));
    }

    [HttpPut("offers/update")]
    public async Task<ActionResult<UserOfferDto>> UpdateUserOffer(UserOfferDto userOfferDto)
    {
        if (await ComplianceCheck(User.GetUserId(), userOfferDto.Id))
            return NotFound(new ApiResponse(404, "The offer not found"));

        var userOffer = await _unitOfWork.Repository<UserOffer>().GetByGuidAsync(userOfferDto.Id);

        _mapper.Map(userOfferDto, userOffer);

        if (await _unitOfWork.Complete())
            return Ok(_mapper.Map<UserOfferDto>(userOffer));

        return BadRequest(new ApiResponse(400, "Failed to update the offer"));
    }

    [HttpDelete("offers/delete/{offerId:guid}")]
    public async Task<ActionResult> DeleteUserOffer(Guid offerId)
    {
        if (await ComplianceCheck(User.GetUserId(), offerId))
            return NotFound(new ApiResponse(404, "The offer not found"));

        var userOffer = await _unitOfWork.Repository<UserOffer>().GetByGuidAsync(offerId);

        _unitOfWork.Repository<UserOffer>().Delete(userOffer);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to delete the offer"));
    }

    #endregion

    #endregion

    #region Specification

    private async Task<Pagination<UserTagDto>> GetAllTagsBySpecificationParams(UserTagParams userTagParams)
    {
        var spec = new UserTagWithSpecificationParams(userTagParams);

        var countSpec = new UserTagWithFiltersForCountSpecification(userTagParams);

        var totalItems = await _unitOfWork.Repository<UserTag>().CountAsync(countSpec);

        var userProfiles = await _unitOfWork.Repository<UserTag>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<UserTag>, IReadOnlyList<UserTagDto>>(userProfiles);

        return new Pagination<UserTagDto>(userTagParams.PageNumber, userTagParams.PageSize, totalItems, data);
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

    private async Task<Pagination<UserOfferDto>> GetUserOffersBySpecificationParams(UserOfferParams userOfferParams)
    {
        var spec = new UserOfferWithSpecificationParams(userOfferParams);

        var countSpec = new UserOfferWithFiltersForCountSpecification(userOfferParams);

        var totalItems = await _unitOfWork.Repository<UserOffer>().CountAsync(countSpec);

        var userOffers = await _unitOfWork.Repository<UserOffer>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<UserOffer>, IReadOnlyList<UserOfferDto>>(userOffers);

        return new Pagination<UserOfferDto>(userOfferParams.PageNumber, userOfferParams.PageSize, totalItems, data);
    }

    #endregion

    private async Task<bool> ComplianceCheck(Guid userProfileId, Guid offerId)
    {
        var userProfileWithOffer = await _unitOfWork.UserProfileRepository.GetUserProfileByOfferGuid(offerId);

        if (userProfileId != userProfileWithOffer.Id)
            return true;

        return false;
    }
}