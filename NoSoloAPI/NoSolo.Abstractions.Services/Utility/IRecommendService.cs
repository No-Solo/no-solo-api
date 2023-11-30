using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Abstractions.Services.Users;

public interface IRecommendService
{
    Task<IReadOnlyList<User>> RecommendUsersForOrganizationOfferByTags(List<string> tags);
    Task<IReadOnlyList<Organization>> RecommendOrganizationsForUserOfferByTags(List<string> tags);
}