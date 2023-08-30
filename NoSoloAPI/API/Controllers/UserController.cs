using API.Dtos;
using API.Errors;
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

    public UserController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("profile")]
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
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithIncludesAsync(User.Identity.Name);

        _mapper.Map(userProfileDto, user.UserProfile);
        
        _unitOfWork.UserRepository.Update(user);

        if (await _unitOfWork.Complete())
            return Ok(user.UserProfile);

        return BadRequest(new ApiResponse(400, "Failed to update user"));
    }
}