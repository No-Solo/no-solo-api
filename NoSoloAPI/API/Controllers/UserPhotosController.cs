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
public class UserPhotosController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public UserPhotosController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _photoService = photoService;
    }
    
    
    [HttpGet("photo")]
    public async Task<ActionResult<UserProfilePhotoDto>> GetUserProfilePhoto()
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithPhotoIncludeAsync(User.GetUsername());

        if (userProfile.Photo == null)
            return NoContent();
        
        return Ok(_mapper.Map<UserProfilePhotoDto>(userProfile.Photo));
    }

    [HttpPost("add")]
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

    [HttpDelete("delete")]
    public async Task<ActionResult> DeletePhoto()
    {
        var userProfile = await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithPhotoIncludeAsync(User.GetUsername());

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
}