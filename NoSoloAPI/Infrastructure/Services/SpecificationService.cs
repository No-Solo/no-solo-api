using AutoMapper;
using Core.Entities;
using Core.Interfaces.Data;
using Core.Interfaces.Services;
using Core.Specification.UserContact;
using Core.Specification.UserOffer;
using Core.Specification.UserTag;

namespace Infrastructure.Services;

public class SpecificationService : ISpecificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SpecificationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Pagination<UserTagDto>> GetAllTagsBySpecificationParams(UserTagParams userTagParams)
    {
        var spec = new UserTagWithSpecificationParams(userTagParams);

        var countSpec = new UserTagWithFiltersForCountSpecification(userTagParams);

        var totalItems = await _unitOfWork.Repository<UserTag>().CountAsync(countSpec);

        var userProfiles = await _unitOfWork.Repository<UserTag>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<UserTag>, IReadOnlyList<UserTagDto>>(userProfiles);

        return new Pagination<UserTagDto>(userTagParams.PageNumber, userTagParams.PageSize, totalItems, data);
    }

    public async Task<Pagination<ContactDto>> GetUserContactsBySpecificationParams(UserContactParams userContactParams)
    {
        var spec = new UserContactWithSpecificationParams(userContactParams);

        var countSpec = new UserContactWithFiltersForCountSpecification(userContactParams);

        var totalItems = await _unitOfWork.Repository<Contact<UserProfile>>().CountAsync(countSpec);

        var userOffers = await _unitOfWork.Repository<Contact<UserProfile>>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<Contact<UserProfile>>, IReadOnlyList<ContactDto>>(userOffers);

        return new Pagination<ContactDto>(userContactParams.PageNumber, userContactParams.PageSize, totalItems, data);
    }

    public async Task<Pagination<UserOfferDto>> GetUserOffersBySpecificationParams(UserOfferParams userOfferParams)
    {
        var spec = new UserOfferWithSpecificationParams(userOfferParams);

        var countSpec = new UserOfferWithFiltersForCountSpecification(userOfferParams);

        var totalItems = await _unitOfWork.Repository<UserOffer>().CountAsync(countSpec);

        var userOffers = await _unitOfWork.Repository<UserOffer>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<UserOffer>, IReadOnlyList<UserOfferDto>>(userOffers);

        return new Pagination<UserOfferDto>(userOfferParams.PageNumber, userOfferParams.PageSize, totalItems, data);
    }
}