using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UserController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public UserController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _photoService = photoService;
    }

    [HttpGet("profile", Name = "GetUserProfile")]
    public async Task<ActionResult<UserProfileDto>> GetCurrentUserProfile()
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithIncludesAsync(User.Identity.Name);

        var userProfile = _mapper.Map<UserProfileDto>(user.UserProfile);

        return Ok(userProfile);
    }

    [HttpPost("create-profile")]
    public async Task<ActionResult<UserProfileDto>> CreateUserProfile([FromBody] CreateUserProfileDto userProfileDto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithIncludesAsync(User.Identity.Name);

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

        if (userProfileDto.Locale == null)
            userProfile.Locale = LocaleEnum.Ukrainian;

        user.UserProfile = userProfile;
        if (await _unitOfWork.Complete())
            // Ok(new ApiResponse(200, "The user profile successfully created"));
            return Ok(userProfile);
        

        return BadRequest(new ApiResponse(400, "Problem user profile creating"));
    }

    [HttpPut("update-profile")]
    public async Task<ActionResult<UserProfileDto>> UpdateUserProfile([FromBody] UpdateUserProfileDto userProfileDto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithIncludesAsync(User.GetUsername());

        _mapper.Map(userProfileDto, user.UserProfile);
        
        _unitOfWork.UserRepository.Update(user);

        if (await _unitOfWork.Complete())
            return Ok(user.UserProfile);

        return BadRequest(new ApiResponse(400, "Failed to update user"));
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<UserProfilePhotoDto>> AddPhotoToUserProfile(IFormFile file)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithIncludesAsync(User.GetUsername());

        if (user.UserProfile.Photo != null)
            return BadRequest(new ApiResponse(400, "The user already has a photo"));
        
        var result = await _photoService.AddPhotoAsync(file);

        if (result.Error != null) return BadRequest(result.Error.Message);

        var photo = new UserPhoto()
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        user.UserProfile.Photo = photo;

        if (await _unitOfWork.Complete())
            return CreatedAtRoute("GetUserProfile", _mapper.Map<UserProfilePhotoDto>(photo));

        return BadRequest("Problem addding photo");
    }
    
    [HttpDelete("delete-photo")]
    public async Task<ActionResult> DeletePhoto()
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithIncludesAsync(User.GetUsername());

        var photo = user.UserProfile.Photo;

        if (photo == null) 
            return NotFound(new ApiResponse(404));

        if (photo.PublicId != null)
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null) 
                return BadRequest(new ApiResponse(400, result.Error.Message));
        }

        user.UserProfile.Photo = null;

        if (await _unitOfWork.Complete()) 
            return Ok();

        return BadRequest("Failed to delete the photo");
    }
}