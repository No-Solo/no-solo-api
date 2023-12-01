using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Contracts.Dtos.Users.Offers;
using NoSolo.Core.Specification.Organization.OrganizationOffer;
using NoSolo.Core.Specification.Users.UserOffer;

namespace NoSolo.Abstractions.Services.Utility;

public interface IRecommendService
{
    Task<Pagination<UserOfferDto>> RecommendUsersForOrganizationOfferByTags(UserOfferParams userOfferParams);
    Task<Pagination<OrganizationOfferDto>> RecommendOrganizationsForUserOfferByTags(OrganizationOfferParams organizationOfferParams);
}