using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Base.Create;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Specification.OrganizationContact;
using NoSolo.Core.Specification.Users.UserContact;

namespace NoSolo.Abstractions.Services.Contacts;

public interface IContactService
{
    Task<ContactDto> Add(Organization organization, NewContactDto contactDto);
    Task<ContactDto> Add(User user, NewContactDto contactDto);
    Task<Contact<Organization>> Get(Organization organization, Guid contactGuid);
    Task<ContactDto> GetDto(Organization organization, Guid contactGuid);
    Task<Pagination<ContactDto>> Get(OrganizationContactParams organizationContactParams);
    Task<Pagination<ContactDto>> Get(UserContactParams userContactParams);
    Task<Contact<User>> Get(User user, Guid contactGuid);
    Task<ContactDto> GetDto(User user, Guid contactGuid);
    Task<ContactDto> Update(Organization organization, ContactDto contactDto);
    Task<ContactDto> Update(User user, ContactDto contactDto);
    Task Delete(Organization organization, Guid contactGuid);
    Task Delete(User user, Guid contactGuid);
}