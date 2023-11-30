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
        CreateMap<Contact<Organization>, ContactDto>();
        CreateMap<ContactDto, Contact<Organization>>();
        
        CreateMap<Contact<User>, ContactDto>();
        CreateMap<ContactDto, Contact<User>>();
    }
}