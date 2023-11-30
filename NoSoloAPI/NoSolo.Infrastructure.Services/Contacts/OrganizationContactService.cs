using AutoMapper;
using NoSolo.Abstractions.Services.Contacts;
using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Organizations;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Base.Create;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.OrganizationContact;

namespace NoSolo.Infrastructure.Services.Contacts;

public class OrganizationContactService : IOrganizationContactService
{
    private readonly IOrganizationService _organizationService;
    private readonly IMemberService _memberService;
    private readonly IContactService _contactService;

    public OrganizationContactService(IOrganizationService organizationService, IMemberService memberService,
        IContactService contactService)
    {
        _organizationService = organizationService;
        _memberService = memberService;
        _contactService = contactService;
    }

    public async Task<ContactDto> Add(NewContactDto contactDto, Guid organizationGuid, string email)
    {
        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Owner },
                organizationGuid, email))
            throw new NotAccessException();

        var organization =
            await _organizationService.Get(organizationGuid, OrganizationIncludeEnum.Contacts);

        return await _contactService.Add(organization, contactDto);
    }

    public async Task<Pagination<ContactDto>> Get(OrganizationContactParams organizationContactParams,
        Guid organizationGuid)
    {
        organizationContactParams.OrganizationId = organizationGuid;

        return await _contactService.Get(organizationContactParams);
    }

    public async Task<ContactDto> Get(Guid contactGuid, Guid organizationGuid)
    {
        var organization =
            await _organizationService.Get(organizationGuid, OrganizationIncludeEnum.Contacts);

        return await _contactService.GetDto(organization, contactGuid);
    }

    public async Task<ContactDto> Update(ContactDto contactDto, Guid organizationGuid, string email)
    {
        var organization =
            await _organizationService.Get(organizationGuid, OrganizationIncludeEnum.Contacts);

        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Owner },
                organizationGuid, email))
            throw new NotAccessException();

        return await _contactService.Update(organization, contactDto);
    }

    public async Task Delete(Guid contactGuid, Guid organizationGuid, string email)
    {
        var organization =
            await _organizationService.Get(organizationGuid, OrganizationIncludeEnum.Contacts);

        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Owner },
                organizationGuid, email))
            throw new NotAccessException();

        await _contactService.Delete(organization, contactGuid);
    }
}