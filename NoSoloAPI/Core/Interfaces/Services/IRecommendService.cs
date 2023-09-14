using Core.Entities;
using Core.Enums;

namespace Core.Interfaces.Services;

public interface IRecommendService
{
    Task<IReadOnlyList<UserProfile>> RecommendUsersForOrganizationOfferByTags(List<TagEnum> tags);
    Task<IReadOnlyList<Organization>> RecommendOrganizationsForUserOfferByTags(List<TagEnum> tags);
}