using AutoMapper;
using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Services.Offers;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Contracts.Dtos.User;
using NoSolo.Contracts.Dtos.User.Create;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.OrganizationOffer;
using NoSolo.Core.Specification.User.UserOffer;
using NoSolo.Core.Specification.Users.UserOffer;

namespace NoSolo.Infrastructure.Services.Offers;

public class OfferService : IOfferService
{
    private readonly IGenericRepository<OrganizationOffer> _organizationOfferRepository;
    private readonly IGenericRepository<UserOffer> _userOfferRepository;
    private readonly IMapper _mapper;

    public OfferService(IGenericRepository<OrganizationOffer> organizationOfferRepository, IGenericRepository<UserOffer> userOfferRepository, IMapper mapper)
    {
        _organizationOfferRepository = organizationOfferRepository;
        _userOfferRepository = userOfferRepository;
        _mapper = mapper;
    }
    
    public async Task<OrganizationOfferDto> Add(Organization organization, NewOrganizationOfferDto organizationOfferDto)
    {
        var organizationOffer = new OrganizationOffer
        {
            Name = organizationOfferDto.Name,
            Description = organizationOfferDto.Description,
            Tags = organizationOfferDto.Tags,
            Organization = organization,
            OrganizationId = organization.Id
        };

        _organizationOfferRepository.AddAsync(organizationOffer);
        _organizationOfferRepository.Save();

        return _mapper.Map<OrganizationOfferDto>(organizationOffer);
    }

    public async Task<UserOfferDto> Add(User user, NewUserOfferDto userOfferDto)
    {
        var userOffer = new UserOffer()
        {
            Preferences = userOfferDto.Preferences,
            User = user,
            UserGuid = user.Id
        };
        
        _userOfferRepository.AddAsync(userOffer);
        _userOfferRepository.Save();

        return _mapper.Map<UserOfferDto>(userOffer);
    }

    public async Task<Pagination<OrganizationOfferDto>> Get(OrganizationOfferParams organizationOfferParams)
    {
        var spec = new OrganizationOfferWithSpecificationParams(organizationOfferParams);

        var countSpec = new OrganizationOfferWithFiltersForCountSpecification(organizationOfferParams);

        var totalItems = await _organizationOfferRepository.CountAsync(countSpec);

        var organizationOffers = await _organizationOfferRepository.ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<OrganizationOffer>, IReadOnlyList<OrganizationOfferDto>>(organizationOffers);

        return new Pagination<OrganizationOfferDto>(organizationOfferParams.PageNumber,
            organizationOfferParams.PageSize, totalItems, data);
    }

    public async Task<Pagination<UserOfferDto>> Get(UserOfferParams userOfferParams)
    {
        var spec = new UserOfferWithSpecificationParams(userOfferParams);

        var countSpec = new UserOfferWithFiltersForCountSpecification(userOfferParams);

        var totalItems = await _userOfferRepository.CountAsync(countSpec);

        var userOffers = await _userOfferRepository.ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<UserOffer>, IReadOnlyList<UserOfferDto>>(userOffers);

        return new Pagination<UserOfferDto>(userOfferParams.PageNumber, userOfferParams.PageSize, totalItems, data);
    }

    public async Task<OrganizationOfferDto> GetOrganizationOfferDto(Guid offerGuid)
    {
        return _mapper.Map<OrganizationOfferDto>(await _organizationOfferRepository.GetByGuidAsync(offerGuid));
    }

    public async Task<UserOfferDto> GetUserOfferDto(Guid offerGuid)
    {
        return _mapper.Map<UserOfferDto>(await _userOfferRepository.GetByGuidAsync(offerGuid));
    }

    public async Task<OrganizationOffer> Get(Organization organization, Guid offerGuid)
    {
        var offer = organization.Offers.SingleOrDefault(o => o.Id == offerGuid);
        if (offer is null)
            throw new EntityNotFound("The offer is not found");

        return offer;
    }

    public async Task<UserOffer> Get(User user, Guid offerGuid)
    {
        var offer = user.Offers.SingleOrDefault(o => o.Id == offerGuid);
        if (offer is null)
            throw new EntityNotFound("The offer is not found");

        return offer;
    }

    public async Task<OrganizationOfferDto> Update(Organization organization, OrganizationOfferDto organizationOfferDto)
    {
        var offer = await Get(organization, organizationOfferDto.Id);
        
        _mapper.Map(organizationOfferDto, offer);
        _organizationOfferRepository.Save();
        
        return _mapper.Map<OrganizationOfferDto>(offer);
    }

    public async Task<UserOfferDto> Update(User user, UserOfferDto userOfferDto)
    {
        var offer = await Get(user, userOfferDto.Id);
        
        _mapper.Map(userOfferDto, offer);
        _userOfferRepository.Save();
        
        return _mapper.Map<UserOfferDto>(offer);
    }

    public async Task Delete(Organization organization, Guid offerGuid)
    {
        var offer = await Get(organization, offerGuid);
        
        _organizationOfferRepository.Delete(offer);
        _organizationOfferRepository.Save();
    }

    public async Task Delete(User user, Guid offerGuid)
    {
        var offer = await Get(user, offerGuid);
        
        _userOfferRepository.Delete(offer);
        _userOfferRepository.Save();
    }
}