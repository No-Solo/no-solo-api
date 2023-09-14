using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // User
        CreateMap<User, UserDto>();
        // User Profile
        CreateMap<UserProfile, UserProfileDto>();
        CreateMap<UpdateUserProfileDto, UserProfile>();
        // Tags
        CreateMap<CreateUserTagDto, UserTag>();
        CreateMap<UserTag, UserTagDto>();
        CreateMap<UserTagDto, UserTag>();
        // Contacts
        CreateMap<Contact<UserProfile>, ContactDto>();
        CreateMap<Contact<Organization>, ContactDto>();
        CreateMap<ContactDto, Contact<Organization>>();
        // Offers
        CreateMap<UserOffer, UserOfferDto>();
        CreateMap<CreateUserOfferDto, UserOffer>();
        CreateMap<OrganizationOffer, OrganizationOfferDto>();
        CreateMap<OrganizationOfferDto, OrganizationOffer>();
        // Organizations
        CreateMap<Organization, OrganizationDto>()
            .ForMember(dest => dest.PhotoUrl,
                opt => opt
                    .MapFrom(src => src.Photos
                        .FirstOrDefault(x => x.IsMain).Url));
        CreateMap<CreateOrganizationDto, Organization>();
        CreateMap<OrganizationUser, OrganizationUserDto>();
        CreateMap<UpdateOrganizationDto, Organization>();
        // Projects
        CreateMap<Project, ProjectDto>();
        // Photos
        CreateMap<UserPhoto, UserProfilePhotoDto>();
        CreateMap<OrganizationPhoto, OrganizationPhotoDto>();
    }
}