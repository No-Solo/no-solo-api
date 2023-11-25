using AutoMapper;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Organization.Update;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Contracts.Dtos.Organizations.Organizations;
using NoSolo.Contracts.Dtos.Organizations.Photos;
using NoSolo.Contracts.Dtos.User;
using NoSolo.Contracts.Dtos.User.Create;
using NoSolo.Contracts.Dtos.Users;
using NoSolo.Contracts.Dtos.Users.Photo;
using NoSolo.Contracts.Dtos.Users.Tags;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Web.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // User
        CreateMap<User, UserDto>();
        // Tags
        CreateMap<NewUserTagDto, UserTag>();
        CreateMap<UserTag, UserTagDto>();
        CreateMap<UserTagDto, UserTag>();
        // Contacts
        CreateMap<Contact<User>, ContactDto>();
        CreateMap<Contact<Organization>, ContactDto>();
        CreateMap<ContactDto, Contact<Organization>>();
        // Offers
        CreateMap<UserOffer, UserOfferDto>();
        CreateMap<NewUserOfferDto, UserOffer>();
        CreateMap<OrganizationOffer, OrganizationOfferDto>();
        CreateMap<OrganizationOfferDto, OrganizationOffer>();
        // Organizations
        CreateMap<Organization, OrganizationDto>()
            .ForMember(dest => dest.PhotoUrl,
                opt => opt
                    .MapFrom(src => src.Photos
                        .FirstOrDefault(x => x.IsMain).Url));
        CreateMap<NewOrganizationDto, Organization>();
        CreateMap<Member, OrganizationUserDto>();
        CreateMap<UpdateOrganizationDto, Organization>();
        // Photos
        CreateMap<UserPhoto, UserPhotoDto>();
        CreateMap<OrganizationPhoto, OrganizationPhotoDto>();
    }
}