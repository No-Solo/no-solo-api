using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Contacts;
using NoSolo.Core.Specification.Users.UserContact;

namespace NoSolo.Abstractions.Services.Contacts;

public interface IUserContactService
{
    Task<ContactDto> Add(NewContactDto contactDto, string email);

    Task<Pagination<ContactDto>> Get(UserContactParams userContactParams, Guid? guid);
    Task<ContactDto> Get(Guid contactGuid, string email);

    Task<ContactDto> Update(ContactDto contactDto, string email);

    Task Delete(Guid contactGuid, string email);
}