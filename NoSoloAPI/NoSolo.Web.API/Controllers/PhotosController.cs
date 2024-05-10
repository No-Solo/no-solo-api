using System.Diagnostics.CodeAnalysis;
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
[ExcludeFromCodeCoverage]
public class PhotosController(IUserPhotoService userPhotoService, IOrganizationPhotoService organizationPhotoService)
    : BaseApiController
{
    #region UserEntity

    [Authorize]
    [HttpGet("userEntity/my")]
    public async Task<UserPhotoDto> GetUserProfilePhoto()
    {
        return await userPhotoService.Get(User.GetEmail());
    }

    [HttpGet("userEntity/{email}")]
    public async Task<UserPhotoDto> GetUserProfilePhoto(string email)
    {
        return await userPhotoService.Get(email);
    }

    [Authorize]
    [HttpPost("userEntity/my/add")]
    public async Task<UserPhotoDto> AddPhotoToUserProfile(IFormFile file)
    {
        return await userPhotoService.Add(file, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("userEntity/my/delete")]
    public async Task DeletePhoto()
    {
        await userPhotoService.DeleteUserPhoto(User.GetEmail());
    }

    #endregion

    #region OrganizationEntity

    [HttpGet("organizationEntity/{organizationId:guid}/main")]
    public async Task<OrganizationPhotoDto> GetMainOrganizationPhoto(Guid organizationId)
    {
        return await organizationPhotoService.GetMain(organizationId);
    }

    [HttpGet("organizationEntity/{organizationId:guid}/all")]
    public async Task<Pagination<OrganizationPhotoDto>> GetOrganizationPhotosWithParams(
        Guid organizationId)
    {
        return await organizationPhotoService.GetAll(organizationId);
    }

    [Authorize]
    [HttpPost("organizationEntity/{organizationId:guid}/add")]
    public async Task<OrganizationPhotoDto> AddPhotoToOrganization(IFormFile file, Guid organizationId)
    {
        return await organizationPhotoService.Add(file, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpPut("organizationEntity/{organizationId:guid}/set-main-photo/{photoId}")]
    public async Task<ActionResult<OrganizationPhotoDto>> SetMainPhoto(Guid organizationId, Guid photoId)
    {
        return await organizationPhotoService.SetMainPhoto(photoId, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("organizationEntity/{organizationId:guid}/delete/{photoId}")]
    public async Task DeletePhoto(Guid organizationId, Guid photoId)
    {
        await organizationPhotoService.Delete(photoId, organizationId, User.GetEmail());
    }

    #endregion
}