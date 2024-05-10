using NoSolo.Abstractions.Services.Contacts;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Contacts;
using NoSolo.Core.Enums;
using NoSolo.Core.Specification.Users.UserContact;

namespace NoSolo.Infrastructure.Services.Contacts;

public class UserContactService(IUserService userService, IContactService contactService) : IUserContactService
{
    public async Task<ContactDto> Add(NewContactDto contactDto, string email)
    {
        var user = await userService.GetUser(email, UserInclude.Contacts);

        return contactService.Add(user, contactDto);
    }

    public async Task<Pagination<ContactDto>> Get(UserContactParams userContactParams, Guid? guid)
    {
        userContactParams.UserGuid = guid;

        return await contactService.Get(userContactParams);
    }

    public async Task<ContactDto> Get(Guid contactGuid, string email)
    {
        var user = await userService.GetUser(email, UserInclude.Contacts);

        return contactService.GetDto(user, contactGuid);
    }

    public async Task<ContactDto> Update(ContactDto contactDto, string email)
    {
        var user = await userService.GetUser(email, UserInclude.Contacts);

        return contactService.Update(user, contactDto);
    }

    public async Task Delete(Guid contactGuid, string email)
    {
        var user = await userService.GetUser(email, UserInclude.Contacts);

        contactService.Delete(user, contactGuid);
    }
}