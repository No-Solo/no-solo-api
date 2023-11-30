using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Abstractions.Services.Utility;

public interface IRecommendService
{
    Task<IReadOnlyList<User>> RecommendUsersForOrganizationOfferByTags(List<string> tags);
    Task<IReadOnlyList<Organization>> RecommendOrganizationsForUserOfferByTags(List<string> tags);
}