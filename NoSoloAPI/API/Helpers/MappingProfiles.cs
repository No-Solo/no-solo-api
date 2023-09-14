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
        CreateMap<UserPhoto, UserProfilePhotoDto>();
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
        // Organizations
        CreateMap<Organization, OrganizationDto>();
        CreateMap<CreateOrganizationDto, Organization>();
        CreateMap<OrganizationUser, OrganizationUserDto>();
        CreateMap<UpdateOrganizationDto, Organization>();
        // Projects
        CreateMap<Project, ProjectDto>();
    }
}