using AutoMapper;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Contracts.Dtos.Users.Offers;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Specification.Organization.OrganizationOffer;
using NoSolo.Core.Specification.Recommendation;
using NoSolo.Core.Specification.Users.UserOffer;

namespace NoSolo.Infrastructure.Services.Utility;

public class RecommendService : IRecommendService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RecommendService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<UserOfferDto>> RecommendUsersForOrganizationOfferByTags(
        UserOfferParams userOfferParams)
    {
        if (userOfferParams.Tags is null)
            return null!;

        var usersOffers = await _unitOfWork.Repository<UserOffer>().ListAllAsync();

        var targetOffers = new List<UserOffer>();

        foreach (var offer in usersOffers)
        {
            var isExist = false;

            foreach (var tag in offer.Tags)
            {
                if (userOfferParams.Tags.Contains(tag))
                    isExist = true;
            }

            if (isExist)
                targetOffers.Add(offer);
        }

        var data = _mapper.Map<IReadOnlyList<UserOffer>, IReadOnlyList<UserOfferDto>>(targetOffers);

        return new Pagination<UserOfferDto>(userOfferParams.PageNumber, userOfferParams.PageSize, targetOffers.Count,
            data);
    }

    public async Task<Pagination<OrganizationOfferDto>> RecommendOrganizationsForUserOfferByTags(
        OrganizationOfferParams organizationOfferParams)
    {
        if (organizationOfferParams.Tags is null)
            return null!;

        var organizationsOffers = await _unitOfWork.Repository<OrganizationOffer>().ListAllAsync();

        var targetOffers = new List<OrganizationOffer>();

        foreach (var offer in organizationsOffers)
        {
            var isExist = false;

            foreach (var tag in offer.Tags)
            {
                if (organizationOfferParams.Tags.Contains(tag))
                    isExist = true;
            }

            if (isExist)
                targetOffers.Add(offer);
        }

        var data = _mapper.Map<IReadOnlyList<OrganizationOffer>, IReadOnlyList<OrganizationOfferDto>>(targetOffers);

        return new Pagination<OrganizationOfferDto>(organizationOfferParams.PageNumber,
            organizationOfferParams.PageSize, targetOffers.Count,
            data);
    }
}