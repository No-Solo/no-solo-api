using NoSolo.Abstractions.Services.Contacts;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Contacts;
using NoSolo.Core.Enums;
using NoSolo.Core.Specification.Users.UserContact;

namespace NoSolo.Infrastructure.Services.Contacts;

public class UserContactService : IUserContactService
{
    private readonly IUserService _userService;
    private readonly IContactService _contactService;

    public UserContactService(IUserService userService, IContactService contactService)
    {
        _userService = userService;
        _contactService = contactService;
    }

    public async Task<ContactDto> Add(NewContactDto contactDto, string email)
    {
        var user = await _userService.GetUser(email, UserInclude.Contacts);

        return _contactService.Add(user, contactDto);
    }

    public async Task<Pagination<ContactDto>> Get(UserContactParams userContactParams, Guid? guid)
    {
        userContactParams.UserGuid = guid;

        return await _contactService.Get(userContactParams);
    }

    public async Task<ContactDto> Get(Guid contactGuid, string email)
    {
        var user = await _userService.GetUser(email, UserInclude.Contacts);

        return _contactService.GetDto(user, contactGuid);
    }

    public async Task<ContactDto> Update(ContactDto contactDto, string email)
    {
        var user = await _userService.GetUser(email, UserInclude.Contacts);

        return _contactService.Update(user, contactDto);
    }

    public async Task Delete(Guid contactGuid, string email)
    {
        var user = await _userService.GetUser(email, UserInclude.Contacts);

        _contactService.Delete(user, contactGuid);
    }
}