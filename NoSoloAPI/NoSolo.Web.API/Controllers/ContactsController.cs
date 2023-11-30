using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Contacts;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Contacts;
using NoSolo.Core.Specification.Organization.OrganizationContact;
using NoSolo.Core.Specification.Users.UserContact;
using NoSolo.Web.API.Extensions;

namespace NoSolo.Web.API.Controllers;

[AllowAnonymous]
[Route("api/contacts")]
public class ContactsController : BaseApiController
{
    private readonly IUserContactService _userContactService;
    private readonly IOrganizationContactService _organizationContactService;

    public ContactsController(IUserContactService userContactService,
        IOrganizationContactService organizationContactService)
    {
        _userContactService = userContactService;
        _organizationContactService = organizationContactService;
    }

    [AllowAnonymous]
    [HttpGet("user")]
    public async Task<Pagination<ContactDto>> GetUserContacts([FromQuery] UserContactParams userContactParams)
    {
        return await _userContactService.Get(userContactParams, userContactParams.UserGuid);
    }

    [Authorize]
    [HttpGet("user/my")]
    public async Task<Pagination<ContactDto>> GetAuthorizedUserContacts(
        [FromQuery] UserContactParams userContactParams)
    {
        return await _userContactService.Get(userContactParams, User.GetUserId());
    }

    [Authorize]
    [HttpGet("user/my/{contactGuid:guid}")]
    public async Task<ContactDto> GetMyUserProfileContactByGuid(Guid contactGuid)
    {
        return await _userContactService.Get(contactGuid, User.GetEmail());
    }

    [Authorize]
    [HttpPut("user/my/update")]
    public async Task<ContactDto> UpdateUserProfileContact(ContactDto contactDto)
    {
        return await _userContactService.Update(contactDto, User.GetEmail());
    }

    [Authorize]
    [HttpPost("user/my/add")]
    public async Task<ContactDto> AddContactToUserProfile(NewContactDto contactDto)
    {
        return await _userContactService.Add(contactDto, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("user/my/delete/{contactGuid:guid}")]
    public async Task DeleteContactFromUserProfile(Guid contactGuid)
    {
        await _userContactService.Delete(contactGuid, User.GetEmail());
    }


    [AllowAnonymous]
    [HttpGet("organization/{organizationId:guid}")]
    public async Task<Pagination<ContactDto>> GetOrganizationContacts(
        [FromQuery] OrganizationContactParams organizationContactParams, Guid organizationId)
    {
        return await _organizationContactService.Get(organizationContactParams, organizationId);
    }

    [AllowAnonymous]
    [HttpGet("organization/{organizationId:Guid}/{contactId:guid}")]
    public async Task<ContactDto> GetOrganizationContactByGuid(Guid organizationId, Guid contactId)
    {
        return await _organizationContactService.Get(contactId, organizationId);
    }

    [Authorize]
    [HttpPut("organization/{organizationId:guid}/update")]
    public async Task<ContactDto> UpdateUserProfileContact(Guid organizationId, ContactDto contactDto)
    {
        return await _organizationContactService.Update(contactDto, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpPost("organization/{organizationId:guid}/add")]
    public async Task<ContactDto> AddContactToOrganization([FromBody] NewContactDto contactDto,
        Guid organizationId)
    {
        return await _organizationContactService.Add(contactDto, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("organization/{organizationId:guid}/delete/{contactId:guid}")]
    public async Task DeleteContactFromUserProfile(Guid organizationId, Guid contactId)
    {
        await _organizationContactService.Delete(contactId, organizationId, User.GetEmail());
    }
}