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
    ContactDto Add(OrganizationEntity organizationEntity, NewContactDto contactDto);
    ContactDto Add(UserEntity userEntity, NewContactDto contactDto);
    ContactEntity<OrganizationEntity> Get(OrganizationEntity organizationEntity, Guid contactGuid);
    ContactDto GetDto(OrganizationEntity organizationEntity, Guid contactGuid);
    Task<Pagination<ContactDto>> Get(OrganizationContactParams organizationContactParams);
    Task<Pagination<ContactDto>> Get(UserContactParams userContactParams);
    ContactEntity<UserEntity> Get(UserEntity userEntity, Guid contactGuid);
    ContactDto GetDto(UserEntity userEntity, Guid contactGuid);
    ContactDto Update(OrganizationEntity organizationEntity, ContactDto contactDto);
    ContactDto Update(UserEntity userEntity, ContactDto contactDto);
    void Delete(OrganizationEntity organizationEntity, Guid contactGuid);
    void Delete(UserEntity userEntity, Guid contactGuid);
}