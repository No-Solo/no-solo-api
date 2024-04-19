using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Tags;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Users.Tags;
using NoSolo.Core.Specification.Users.UserTag;
using NoSolo.Web.API.Extensions;

namespace NoSolo.Web.API.Controllers;

[AllowAnonymous]
[Route("api/userEntity/tags")]
[ExcludeFromCodeCoverage]
public class UserTagsController : BaseApiController
{
    private readonly IUserTagsService _userTagsService;

    public UserTagsController(IUserTagsService userTagsService)
    {
        _userTagsService = userTagsService;
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<Pagination<UserTagDto>> GetAllUserProfileTags(
        [FromQuery] UserTagParams userTagParams)
    {
        userTagParams.UserGuid = User.GetUserId();

        return await _userTagsService.Get(userTagParams);
    }

    [HttpGet("userEntity/{userGuid:guid}")]
    public async Task<Pagination<UserTagDto>> GetAllUserProfileTagsByUserId(Guid userGuid,
        [FromQuery] UserTagParams userTagParams)
    {
        userTagParams.UserGuid = userGuid;

        return await _userTagsService.Get(userTagParams);
    }
    
    [HttpGet("{userTagGuid:guid}")]
    public async Task<UserTagDto> GetUserProfileTagByGuid(Guid userTagGuid)
    {
        return await _userTagsService.Get(userTagGuid);
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<UserTagDto> AddTagToUserProfile([FromBody] NewUserTagDto userTagDto)
    {
        return await _userTagsService.Add(userTagDto, User.GetEmail());
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<UserTagDto> UpdateTag([FromBody] UpdateUserTagDto userTagDto)
    {
        return await _userTagsService.Update(userTagDto, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("delete/{userTagGuid:guid}")]
    public async Task DeleteTagFromUserProfile(Guid userTagGuid)
    {
        await _userTagsService.Delete(userTagGuid, User.GetEmail());
    }

    [Authorize]
    [HttpPost("change-active/{userTagGuid:guid}")]
    public async Task<UserTagDto> ChangeActiveTask(Guid userTagGuid)
    {
        return await _userTagsService.ChangeActiveTask(userTagGuid, User.GetEmail());
    }
}