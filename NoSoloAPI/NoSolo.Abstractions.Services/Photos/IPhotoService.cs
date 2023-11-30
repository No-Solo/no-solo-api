using Microsoft.AspNetCore.Http;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Organizations.Photos;
using NoSolo.Contracts.Dtos.Users.Photo;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Specification.Organization.OrganizationPhotoParams;

namespace NoSolo.Abstractions.Services.Photos;

public interface IPhotoService
{
    Task<OrganizationPhotoDto> Add(Organization organization, IFormFile file);
    Task<UserPhotoDto> Add(User user, IFormFile file);
    Task<OrganizationPhotoDto> GetMainDto(Organization organization);
    Task<UserPhotoDto> GetMainDto(User user);
    Task<OrganizationPhoto> GetMain(Organization organization);
    Task<OrganizationPhoto> Get(Organization organization, Guid photoGuid);
    Task<Pagination<OrganizationPhotoDto>> Get(OrganizationPhotoParams organizationPhotoParams);
    Task<OrganizationPhotoDto> SetMainPhoto(Organization organization, Guid photoGuid);
    Task Delete(Organization organization, Guid photoGuid);
    Task Delete(User user);
}