using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Contacts;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Contacts;
using NoSolo.Core.Specification.Organization.OrganizationContact;
using NoSolo.Core.Specification.Users.UserContact;
using NoSolo.Web.API.Extensions;

namespace NoSolo.Web.API.Controllers;

[AllowAnonymous]
[Route("api/contacts")]
[ExcludeFromCodeCoverage]
public class ContactsController(
    IUserContactService userContactService,
    IOrganizationContactService organizationContactService)
    : BaseApiController
{
    [AllowAnonymous]
    [HttpGet("userEntity")]
    public async Task<Pagination<ContactDto>> GetUserContacts([FromQuery] UserContactParams userContactParams)
    {
        return await userContactService.Get(userContactParams, userContactParams.UserGuid);
    }

    [Authorize]
    [HttpGet("userEntity/my")]
    public async Task<Pagination<ContactDto>> GetAuthorizedUserContacts(
        [FromQuery] UserContactParams userContactParams)
    {
        return await userContactService.Get(userContactParams, User.GetUserId());
    }

    [Authorize]
    [HttpGet("userEntity/my/{contactGuid:guid}")]
    public async Task<ContactDto> GetMyUserProfileContactByGuid(Guid contactGuid)
    {
        return await userContactService.Get(contactGuid, User.GetEmail());
    }

    [Authorize]
    [HttpPut("userEntity/my/update")]
    public async Task<ContactDto> UpdateUserProfileContact(ContactDto contactDto)
    {
        return await userContactService.Update(contactDto, User.GetEmail());
    }

    [Authorize]
    [HttpPost("userEntity/my/add")]
    public async Task<ContactDto> AddContactToUserProfile(NewContactDto contactDto)
    {
        return await userContactService.Add(contactDto, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("userEntity/my/delete/{contactGuid:guid}")]
    public async Task DeleteContactFromUserProfile(Guid contactGuid)
    {
        await userContactService.Delete(contactGuid, User.GetEmail());
    }


    [AllowAnonymous]
    [HttpGet("organizationEntity/")]
    public async Task<Pagination<ContactDto>> GetOrganizationContacts(
        [FromQuery] OrganizationContactParams organizationContactParams)
    {
        return await organizationContactService.Get(organizationContactParams);
    }

    [AllowAnonymous]
    [HttpGet("organizationEntity/{organizationId:Guid}/{contactId:guid}")]
    public async Task<ContactDto> GetOrganizationContactByGuid(Guid organizationId, Guid contactId)
    {
        return await organizationContactService.Get(contactId, organizationId);
    }

    [Authorize]
    [HttpPut("organizationEntity/{organizationId:guid}/update")]
    public async Task<ContactDto> UpdateUserProfileContact(Guid organizationId, ContactDto contactDto)
    {
        return await organizationContactService.Update(contactDto, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpPost("organizationEntity/{organizationId:guid}/add")]
    public async Task<ContactDto> AddContactToOrganization([FromBody] NewContactDto contactDto,
        Guid organizationId)
    {
        return await organizationContactService.Add(contactDto, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("organizationEntity/{organizationId:guid}/delete/{contactId:guid}")]
    public async Task DeleteContactFromUserProfile(Guid organizationId, Guid contactId)
    {
        await organizationContactService.Delete(contactId, organizationId, User.GetEmail());
    }
}