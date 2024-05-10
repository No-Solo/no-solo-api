using AutoMapper;
using NoSolo.Abstractions.Services.Contacts;
using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Organizations;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Contacts;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.OrganizationContact;

namespace NoSolo.Infrastructure.Services.Contacts;

public class OrganizationContactService(
    IOrganizationService organizationService,
    IMemberService memberService,
    IContactService contactService)
    : IOrganizationContactService
{
    public async Task<ContactDto> Add(NewContactDto contactDto, Guid organizationGuid, string email)
    {
        if (!await memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Owner },
                organizationGuid, email))
            throw new NotAccessException();

        var organization =
            await organizationService.Get(organizationGuid, OrganizationIncludeEnum.Contacts);

        return contactService.Add(organization, contactDto);
    }

    public async Task<Pagination<ContactDto>> Get(OrganizationContactParams organizationContactParams)
    {
        return await contactService.Get(organizationContactParams);
    }

    public async Task<ContactDto> Get(Guid contactGuid, Guid organizationGuid)
    {
        var organization =
            await organizationService.Get(organizationGuid, OrganizationIncludeEnum.Contacts);

        return contactService.GetDto(organization, contactGuid);
    }

    public async Task<ContactDto> Update(ContactDto contactDto, Guid organizationGuid, string email)
    {
        var organization =
            await organizationService.Get(organizationGuid, OrganizationIncludeEnum.Contacts);

        if (!await memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Owner },
                organizationGuid, email))
            throw new NotAccessException();

        return contactService.Update(organization, contactDto);
    }

    public async Task Delete(Guid contactGuid, Guid organizationGuid, string email)
    {
        var organization =
            await organizationService.Get(organizationGuid, OrganizationIncludeEnum.Contacts);

        if (!await memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Owner },
                organizationGuid, email))
            throw new NotAccessException();

        contactService.Delete(organization, contactGuid);
    }
}