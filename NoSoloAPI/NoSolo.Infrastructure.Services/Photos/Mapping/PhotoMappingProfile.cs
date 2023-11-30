using AutoMapper;
using NoSolo.Contracts.Dtos.Organizations.Photos;
using NoSolo.Contracts.Dtos.Users.Photo;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Services.Photos.Mapping;

public class PhotoMappingProfile : Profile
{
    public PhotoMappingProfile()
    {
        CreateMap<UserPhoto, UserPhotoDto>();
        
        CreateMap<OrganizationPhoto, OrganizationPhotoDto>();
    }
}