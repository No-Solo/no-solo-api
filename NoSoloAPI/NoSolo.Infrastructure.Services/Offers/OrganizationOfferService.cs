using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Offers;
using NoSolo.Abstractions.Services.Organizations;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.OrganizationOffer;

namespace NoSolo.Infrastructure.Services.Offers;

public class OrganizationOfferService : IOrganizationOfferService
{
    private readonly IOrganizationService _organizationService;
    private readonly IMemberService _memberService;
    private readonly IOfferService _offerService;

    public OrganizationOfferService(IOrganizationService organizationService,
        IMemberService memberService, IOfferService offerService)
    {
        _organizationService = organizationService;
        _memberService = memberService;
        _offerService = offerService;
    }

    public async Task<OrganizationOfferDto> Add(NewOrganizationOfferDto organizationOfferDto, Guid organizationGuid,
        string email)
    {
        if (!await _memberService.MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organizationGuid,
                email))
            throw new NotAccessException();

        var organization = await _organizationService.Get(organizationGuid, OrganizationIncludeEnum.Offers);

        return await _offerService.Add(organization, organizationOfferDto);
    }

    public async Task<Pagination<OrganizationOfferDto>> Get(OrganizationOfferParams organizationOfferParams)
    {
        return await _offerService.Get(organizationOfferParams);
    }

    public async Task<Pagination<OrganizationOfferDto>> Get(OrganizationOfferParams organizationOfferParams,
        Guid organizationGuid)
    {
        organizationOfferParams.OrganizationId = organizationGuid;

        return await _offerService.Get(organizationOfferParams);
    }

    public async Task<OrganizationOfferDto> GetOrganizationOffer(Guid offerGuid)
    {
        return await _offerService.GetOrganizationOfferDto(offerGuid);
    }

    public async Task<OrganizationOfferDto> Update(OrganizationOfferDto organizationOfferDto, Guid organizationGuid,
        string email)
    {
        if (!await _memberService.MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organizationGuid,
                email))
            throw new NotAccessException();

        var organization = await _organizationService.Get(organizationGuid, OrganizationIncludeEnum.Offers);

        return await _offerService.Update(organization, organizationOfferDto);
    }

    public async Task Delete(Guid offerGuid, Guid organizationGuid, string email)
    {
        var organization = await _organizationService.Get(organizationGuid, OrganizationIncludeEnum.Offers);

        if (!await _memberService.MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organizationGuid,
                email))
            throw new NotAccessException();

        await _offerService.Delete(organization, offerGuid);
    }
}