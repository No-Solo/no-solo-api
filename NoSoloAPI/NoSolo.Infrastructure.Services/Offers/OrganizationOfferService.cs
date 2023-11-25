using AutoMapper;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Offers;
using NoSolo.Abstractions.Services.Organizations;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Organization;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.OrganizationOffer;

namespace NoSolo.Infrastructure.Services.Offers;

public class OrganizationOfferService : IOrganizationOfferService
{
    private readonly IOrganizaitonService _organizaitonService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemberService _memberService;

    public OrganizationOfferService(IOrganizaitonService organizaitonService, IUnitOfWork unitOfWork, IMapper mapper,
        IMemberService memberService)
    {
        _organizaitonService = organizaitonService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _memberService = memberService;
    }

    public async Task<OrganizationOfferDto> Add(NewOrganizationOfferDto organizationOfferDto, Guid organizationGuid,
        string email)
    {
        if (!await _memberService.MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organizationGuid,
                email))
            throw new NotAccessException();
        
        var organization = await _organizaitonService.Get(organizationGuid, OrganizationIncludeEnum.Offers);

        var organizationOffer = new OrganizationOffer
        {
            Name = organizationOfferDto.Name,
            Description = organizationOfferDto.Description,
            Tags = organizationOfferDto.Tags
        };

        organization.Offers.Add(organizationOffer);

        return _mapper.Map<OrganizationOfferDto>(organizationOffer);
    }

    public async Task<Pagination<OrganizationOfferDto>> Get(OrganizationOfferParams organizationOfferParams)
    {
        return await GetOrganizationOffersBySpecificationParams(organizationOfferParams);
    }

    public async Task<Pagination<OrganizationOfferDto>> Get(OrganizationOfferParams organizationOfferParams,
        Guid organizationGuid)
    {
        organizationOfferParams.OrganizationId = organizationGuid;

        return await GetOrganizationOffersBySpecificationParams(organizationOfferParams);
    }

    public async Task<OrganizationOfferDto> GetOrganizationOffer(Guid offerGuid)
    {
        var offer = await _unitOfWork.Repository<OrganizationOffer>().GetByGuidAsync(offerGuid);

        return _mapper.Map<OrganizationOfferDto>(offer);
    }

    public async Task<OrganizationOfferDto> Update(OrganizationOfferDto organizationOfferDto, Guid organizationGuid,
        string email)
    {
        if (!await _memberService.MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organizationGuid,
                email))
            throw new NotAccessException();
        
        var organization = await _organizaitonService.Get(organizationGuid, OrganizationIncludeEnum.Offers);

        var offer = organization.Offers.FirstOrDefault(o => o.Id == organizationOfferDto.Id);
        if (offer is null)
            throw new EntityNotFound("The offer is not found");

        _mapper.Map(organizationOfferDto, offer);

        return _mapper.Map<OrganizationOfferDto>(offer);
    }

    public async Task Delete(Guid offerGuid, Guid organizationGuid, string email)
    {
        var organization = await _organizaitonService.Get(organizationGuid, OrganizationIncludeEnum.Offers);

        if (!await _memberService.MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organizationGuid,
                email))
            throw new NotAccessException();

        var offer = organization.Offers.FirstOrDefault(o => o.Id == offerGuid);
        if (offer is null)
            throw new EntityNotFound("The offer is not found");

        organization.Offers.Remove(offer);
    }

    private async Task<Pagination<OrganizationOfferDto>> GetOrganizationOffersBySpecificationParams(
        OrganizationOfferParams organizationOfferParams)
    {
        var spec = new OrganizationOfferWithSpecificationParams(organizationOfferParams);

        var countSpec = new OrganizationOfferWithFiltersForCountSpecification(organizationOfferParams);

        var totalItems = await _unitOfWork.Repository<OrganizationOffer>().CountAsync(countSpec);

        var offers = await _unitOfWork.Repository<OrganizationOffer>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<OrganizationOffer>, IReadOnlyList<OrganizationOfferDto>>(offers);

        return new Pagination<OrganizationOfferDto>(organizationOfferParams.PageNumber,
            organizationOfferParams.PageSize, totalItems, data);
    }
}