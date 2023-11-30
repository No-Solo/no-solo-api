using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Photos;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Organizations.Photos;
using NoSolo.Contracts.Dtos.Users.Photo;
using NoSolo.Web.API.Extensions;

namespace NoSolo.Web.API.Controllers;

[AllowAnonymous]
[Route("api/photos")]
public class PhotosController : BaseApiController
{
    private readonly IUserPhotoService _userPhotoService;
    private readonly IOrganizationPhotoService _organizationPhotoService;

    public PhotosController(IUserPhotoService userPhotoService, IOrganizationPhotoService organizationPhotoService)
    {
        _userPhotoService = userPhotoService;
        _organizationPhotoService = organizationPhotoService;
    }

    #region User

    [Authorize]
    [HttpGet("user/my")]
    public async Task<UserPhotoDto> GetUserProfilePhoto()
    {
        return await _userPhotoService.Get(User.GetEmail());
    }

    [HttpGet("user/{email}")]
    public async Task<UserPhotoDto> GetUserProfilePhoto(string email)
    {
        return await _userPhotoService.Get(email);
    }

    [Authorize]
    [HttpPost("user/my/add")]
    public async Task<UserPhotoDto> AddPhotoToUserProfile(IFormFile file)
    {
        return await _userPhotoService.Add(file, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("user/my/delete")]
    public async Task DeletePhoto()
    {
        await _userPhotoService.DeleteUserPhoto(User.GetEmail());
    }

    #endregion

    #region Organization

    [HttpGet("organization/{organizationId:guid}/main")]
    public async Task<OrganizationPhotoDto> GetMainOrganizationPhoto(Guid organizationId)
    {
        return await _organizationPhotoService.GetMain(organizationId);
    }

    [HttpGet("organization/{organizationId:guid}/all")]
    public async Task<Pagination<OrganizationPhotoDto>> GetOrganizationPhotosWithParams(
        Guid organizationId)
    {
        return await _organizationPhotoService.GetAll(organizationId);
    }

    [Authorize]
    [HttpPost("organization/{organizationId:guid}/add")]
    public async Task<OrganizationPhotoDto> AddPhotoToOrganization(Guid organizationId, IFormFile file)
    {
        return await _organizationPhotoService.Add(file, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpPut("organization/{organizationId:guid}/set-main-photo/{photoId}")]
    public async Task<ActionResult<OrganizationPhotoDto>> SetMainPhoto(Guid organizationId, Guid photoId)
    {
        return await _organizationPhotoService.SetMainPhoto(photoId, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("organization/{organizationId:guid}/delete/{photoId}")]
    public async Task DeletePhoto(Guid organizationId, Guid photoId)
    {
        await _organizationPhotoService.Delete(photoId, organizationId, User.GetEmail());
    }

    #endregion
}