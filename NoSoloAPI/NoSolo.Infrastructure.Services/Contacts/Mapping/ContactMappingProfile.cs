using AutoMapper;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Services.Contacts.Mapping;

public class ContactMappingProfile : Profile
{
    public ContactMappingProfile()
    {
        CreateMap<ContactEntity<OrganizationEntity>, ContactDto>();
        CreateMap<ContactDto, ContactEntity<OrganizationEntity>>();
        
        CreateMap<ContactEntity<UserEntity>, ContactDto>();
        CreateMap<ContactDto, ContactEntity<UserEntity>>();
    }
}