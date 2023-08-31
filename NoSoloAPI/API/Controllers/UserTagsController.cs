using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UserTagsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserTagsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [HttpGet("user-tags", Name = "GetUserProfileTags")]
    public async Task<ActionResult<IReadOnlyList<UserTagDto>>> GetAllUserProfileTags()
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithTagsIncludeAsync(User.GetUsername());

        return Ok(_mapper.Map<IReadOnlyList<UserTagDto>>(userProfile.Tags));
    }

    [HttpPost("add-tag")]
    public async Task<ActionResult> AddTagToUserProfile(CreateUserTagDto userTagDto)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithTagsIncludeAsync(User.GetUsername());

        if (userTagDto != null)
            userProfile.Tags.Add(_mapper.Map<UserTag>(userTagDto));

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Problem adding tag"));
    }
    
    [HttpPut("update-tag")]
    public async Task<ActionResult> UpdateTag(UserTagDto userTagDto)
    {
        var userTag = await _unitOfWork.UserTagRepository.GetUserTagByGuid(userTagDto.Id);

        if (userTag == null)
            return NotFound(new ApiResponse(404, "Tag not found"));

        userTag.Description = userTagDto.Description;
        userTag.Active = userTagDto.Active;
        userTag.Tag = userTagDto.Tag;
        
        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to update the tag"));
    }

    [HttpDelete("delete-tag/{id:guid}")]
    public async Task<ActionResult> DeleteTagFromUserProfile(Guid id)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithTagsIncludeAsync(User.GetUsername());

        var userTag = await _unitOfWork.UserTagRepository.GetUserTagByGuid(id);

        if (!userProfile.Tags.Contains(userTag))
            return NotFound(new ApiResponse(404, "The user profile doesnt have the tag"));

        userProfile.Tags.Remove(userTag);

        if (await _unitOfWork.Complete())
            return Ok();
 
        return BadRequest(new ApiResponse(400, "Failed to delete the tag"));
    }
}