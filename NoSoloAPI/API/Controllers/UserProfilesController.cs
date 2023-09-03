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
public class UserProfilesController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserProfilesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [HttpGet("profile", Name = "GetUserProfile")]
    public async Task<ActionResult<UserProfileDto>> GetCurrentUserProfile()
    {
        var userProfile =
            _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithAllIncludesAsync(User.GetUsername());

        return Ok(_mapper.Map<UserProfileDto>(userProfile));
    }

    [HttpPost("create")]
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
            // Ok(new ApiResponse(200, "The user profile successfully created"));
            return Ok(userProfile);


        return BadRequest(new ApiResponse(400, "Problem user profile creating"));
    }

    [HttpPut("update")]
    public async Task<ActionResult<UserProfileDto>> UpdateUserProfile([FromBody] UpdateUserProfileDto userProfileDto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithAllIncludesAsync(User.GetUsername());

        _mapper.Map(userProfileDto, user.UserProfile);

        _unitOfWork.UserRepository.Update(user);

        if (await _unitOfWork.Complete())
            return Ok(user.UserProfile);

        return BadRequest(new ApiResponse(400, "Failed to update user"));
    }
}