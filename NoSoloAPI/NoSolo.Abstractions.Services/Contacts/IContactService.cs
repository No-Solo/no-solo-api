using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Contacts;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Specification.Organization.OrganizationContact;
using NoSolo.Core.Specification.Users.UserContact;

namespace NoSolo.Abstractions.Services.Contacts;

public interface IContactService
{
    ContactDto Add(Organization organization, NewContactDto contactDto);
    ContactDto Add(User user, NewContactDto contactDto);
    Contact<Organization> Get(Organization organization, Guid contactGuid);
    ContactDto GetDto(Organization organization, Guid contactGuid);
    Task<Pagination<ContactDto>> Get(OrganizationContactParams organizationContactParams);
    Task<Pagination<ContactDto>> Get(UserContactParams userContactParams);
    Contact<User> Get(User user, Guid contactGuid);
    ContactDto GetDto(User user, Guid contactGuid);
    ContactDto Update(Organization organization, ContactDto contactDto);
    ContactDto Update(User user, ContactDto contactDto);
    void Delete(Organization organization, Guid contactGuid);
    void Delete(User user, Guid contactGuid);
}