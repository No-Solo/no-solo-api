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
    Task<OrganizationPhotoDto> Add(OrganizationEntity organizationEntity, IFormFile file);
    Task<UserPhotoDto> Add(UserEntity userEntity, IFormFile file);
    Task<OrganizationPhotoDto> GetMainDto(OrganizationEntity organizationEntity);
    Task<UserPhotoDto> GetMainDto(UserEntity userEntity);
    Task<OrganizationPhotoEntity> GetMain(OrganizationEntity organizationEntity);
    Task<OrganizationPhotoEntity> Get(OrganizationEntity organizationEntity, Guid photoGuid);
    Task<Pagination<OrganizationPhotoDto>> Get(OrganizationPhotoParams organizationPhotoParams);
    Task<OrganizationPhotoDto> SetMainPhoto(OrganizationEntity organizationEntity, Guid photoGuid);
    Task Delete(OrganizationEntity organizationEntity, Guid photoGuid);
    Task Delete(UserEntity userEntity);
}