using AutoMapper;
using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Services.Offers;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Contracts.Dtos.Users.Offers;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.OrganizationOffer;
using NoSolo.Core.Specification.Users.UserOffer;

namespace NoSolo.Infrastructure.Services.Offers;

public class OfferService(
    IRepository<OrganizationOfferEntity> organizationOfferRepository,
    IRepository<UserOfferEntity> userOfferRepository,
    IMapper mapper)
    : IOfferService
{
    public async Task<OrganizationOfferDto> Add(OrganizationEntity organizationEntity, NewOrganizationOfferDto organizationOfferDto)
    {
        var organizationOffer = new OrganizationOfferEntity
        {
            Name = organizationOfferDto.Name,
            Description = organizationOfferDto.Description,
            Tags = organizationOfferDto.Tags,
            Organization = organizationEntity,
            OrganizationId = organizationEntity.Id
        };

        organizationOfferRepository.AddAsync(organizationOffer);
        organizationOfferRepository.Save();

        return mapper.Map<OrganizationOfferDto>(organizationOffer);
    }

    public async Task<UserOfferDto> Add(UserEntity userEntity, NewUserOfferDto userOfferDto)
    {
        var userOffer = new UserOfferEntity()
        {
            Preferences = userOfferDto.Preferences,
            Name = userOfferDto.Name,
            Tags = userOfferDto.Tags,
            UserEntity = userEntity,
            UserGuid = userEntity.Id
        };
        
        userOfferRepository.AddAsync(userOffer);
        userOfferRepository.Save();

        return mapper.Map<UserOfferDto>(userOffer);
    }

    public async Task<Pagination<OrganizationOfferDto>> Get(OrganizationOfferParams organizationOfferParams)
    {
        var spec = new OrganizationOfferWithSpecificationParams(organizationOfferParams);

        var countSpec = new OrganizationOfferWithFiltersForCountSpecification(organizationOfferParams);

        var totalItems = await organizationOfferRepository.CountAsync(countSpec);

        var organizationOffers = await organizationOfferRepository.ListAsync(spec);

        var data = mapper
            .Map<IReadOnlyList<OrganizationOfferEntity>, IReadOnlyList<OrganizationOfferDto>>(organizationOffers);

        return new Pagination<OrganizationOfferDto>(organizationOfferParams.PageNumber,
            organizationOfferParams.PageSize, totalItems, data);
    }

    public async Task<Pagination<UserOfferDto>> Get(UserOfferParams userOfferParams)
    {
        var spec = new UserOfferWithSpecificationParams(userOfferParams);

        var countSpec = new UserOfferWithFiltersForCountSpecification(userOfferParams);

        var totalItems = await userOfferRepository.CountAsync(countSpec);

        var userOffers = await userOfferRepository.ListAsync(spec);

        var data = mapper
            .Map<IReadOnlyList<UserOfferEntity>, IReadOnlyList<UserOfferDto>>(userOffers);

        return new Pagination<UserOfferDto>(userOfferParams.PageNumber, userOfferParams.PageSize, totalItems, data);
    }

    public async Task<OrganizationOfferDto> GetOrganizationOfferDto(Guid offerGuid)
    {
        return mapper.Map<OrganizationOfferDto>(await organizationOfferRepository.GetByGuidAsync(offerGuid));
    }

    public async Task<UserOfferDto> GetUserOfferDto(Guid offerGuid)
    {
        return mapper.Map<UserOfferDto>(await userOfferRepository.GetByGuidAsync(offerGuid));
    }

    public async Task<OrganizationOfferEntity> Get(OrganizationEntity organizationEntity, Guid offerGuid)
    {
        var offer = organizationEntity.Offers.SingleOrDefault(o => o.Id == offerGuid);
        if (offer is null)
            throw new EntityNotFound("The offer is not found");

        return offer;
    }

    public async Task<UserOfferEntity> Get(UserEntity userEntity, Guid offerGuid)
    {
        var offer = userEntity.Offers.SingleOrDefault(o => o.Id == offerGuid);
        if (offer is null)
            throw new EntityNotFound("The offer is not found");

        return offer;
    }

    public async Task<OrganizationOfferDto> Update(OrganizationEntity organizationEntity, OrganizationOfferDto organizationOfferDto)
    {
        var offer = await Get(organizationEntity, organizationOfferDto.Id);
        
        mapper.Map(organizationOfferDto, offer);
        organizationOfferRepository.Save();
        
        return mapper.Map<OrganizationOfferDto>(offer);
    }

    public async Task<UserOfferDto> Update(UserEntity userEntity, UserOfferDto userOfferDto)
    {
        var offer = await Get(userEntity, userOfferDto.Id);
        
        mapper.Map(userOfferDto, offer);
        userOfferRepository.Save();
        
        return mapper.Map<UserOfferDto>(offer);
    }

    public async Task Delete(OrganizationEntity organizationEntity, Guid offerGuid)
    {
        var offer = await Get(organizationEntity, offerGuid);
        
        organizationOfferRepository.Delete(offer);
        organizationOfferRepository.Save();
    }

    public async Task Delete(UserEntity userEntity, Guid offerGuid)
    {
        var offer = await Get(userEntity, offerGuid);
        
        userOfferRepository.Delete(offer);
        userOfferRepository.Save();
    }
}