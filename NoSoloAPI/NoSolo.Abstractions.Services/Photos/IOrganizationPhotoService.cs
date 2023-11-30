using Microsoft.AspNetCore.Http;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Organizations.Photos;

namespace NoSolo.Abstractions.Services.Photos;

public interface IOrganizationPhotoService
{
    Task<OrganizationPhotoDto> Add(IFormFile file, Guid organizationGuid, string email);
    Task<OrganizationPhotoDto> GetMain(Guid organizationGuid);
    Task<Pagination<OrganizationPhotoDto>> GetAll(Guid organizationGuid);
    Task<OrganizationPhotoDto> SetMainPhoto(Guid photoGuid, Guid organizationGuid, string email);
    Task Delete(Guid photoGuid, Guid organizationGuid, string email);
}