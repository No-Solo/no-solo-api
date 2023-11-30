using AutoMapper;
using NoSolo.Abstractions.Services.Contacts;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Base.Create;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Specification.Users.UserContact;

namespace NoSolo.Infrastructure.Services.Contacts;

public class UserContactService : IUserContactService
{
    private readonly IUserService _userService;
    private readonly IContactService _contactService;

    private User? _user;

    public UserContactService(IUserService userService, IContactService contactService)
    {
        _userService = userService;
        _contactService = contactService;

        _user = null;
    }

    public async Task<ContactDto> Add(NewContactDto contactDto, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Contacts);

        return await _contactService.Add(_user, contactDto);
    }

    public async Task<Pagination<ContactDto>> Get(UserContactParams userContactParams, Guid? guid)
    {
        userContactParams.UserGuid = guid;

        return await _contactService.Get(userContactParams);
    }

    public async Task<ContactDto> Get(Guid contactGuid, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Contacts);

        return await _contactService.GetDto(_user, contactGuid);
    }

    public async Task<ContactDto> Update(ContactDto contactDto, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Contacts);

        return await _contactService.Update(_user, contactDto);
    }

    public async Task Delete(Guid contactGuid, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Contacts);

        await _contactService.Delete(_user, contactGuid);
    }
}