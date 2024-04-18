using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Photos;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
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

    #region UserEntity

    [Authorize]
    [HttpGet("userEntity/my")]
    public async Task<UserPhotoDto> GetUserProfilePhoto()
    {
        return await _userPhotoService.Get(User.GetEmail());
    }

    [HttpGet("userEntity/{email}")]
    public async Task<UserPhotoDto> GetUserProfilePhoto(string email)
    {
        return await _userPhotoService.Get(email);
    }

    [Authorize]
    [HttpPost("userEntity/my/add")]
    public async Task<UserPhotoDto> AddPhotoToUserProfile(IFormFile file)
    {
        return await _userPhotoService.Add(file, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("userEntity/my/delete")]
    public async Task DeletePhoto()
    {
        await _userPhotoService.DeleteUserPhoto(User.GetEmail());
    }

    #endregion

    #region OrganizationEntity

    [HttpGet("organizationEntity/{organizationId:guid}/main")]
    public async Task<OrganizationPhotoDto> GetMainOrganizationPhoto(Guid organizationId)
    {
        return await _organizationPhotoService.GetMain(organizationId);
    }

    [HttpGet("organizationEntity/{organizationId:guid}/all")]
    public async Task<Pagination<OrganizationPhotoDto>> GetOrganizationPhotosWithParams(
        Guid organizationId)
    {
        return await _organizationPhotoService.GetAll(organizationId);
    }

    [Authorize]
    [HttpPost("organizationEntity/{organizationId:guid}/add")]
    public async Task<OrganizationPhotoDto> AddPhotoToOrganization(IFormFile file, Guid organizationId)
    {
        return await _organizationPhotoService.Add(file, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpPut("organizationEntity/{organizationId:guid}/set-main-photo/{photoId}")]
    public async Task<ActionResult<OrganizationPhotoDto>> SetMainPhoto(Guid organizationId, Guid photoId)
    {
        return await _organizationPhotoService.SetMainPhoto(photoId, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("organizationEntity/{organizationId:guid}/delete/{photoId}")]
    public async Task DeletePhoto(Guid organizationId, Guid photoId)
    {
        await _organizationPhotoService.Delete(photoId, organizationId, User.GetEmail());
    }

    #endregion
}