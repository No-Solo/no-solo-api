using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Abstractions.Services.Users;

public interface IRecommendService
{
    Task<IReadOnlyList<UserProfile>> RecommendUsersForOrganizationOfferByTags(List<TagEnum> tags);
    Task<IReadOnlyList<Organization>> RecommendOrganizationsForUserOfferByTags(List<TagEnum> tags);
}