using NoSolo.Core.Entities;
using NoSolo.Core.Entities.User;

namespace NoSolo.Abstractions.Repositories;

public interface IUserProfileRepository
{
    Task<UserProfile> GetUserProfileWithoutIncludesAsync(string username);
    Task<UserProfile> GetUserProfileByUsernameWithAllIncludesAsync(string username);
    Task<UserProfile> GetUserProfileByUsernameWithTagsIncludeAsync(string username);
    Task<UserProfile> GetUserProfileByUsernameWithPhotoIncludeAsync(string username);
    Task<UserProfile> GetUserProfileByUsernameWithContactsIncludeAsync(string username);
    Task<UserProfile> GetUserProfileByUsernameWithOffersIncludeAsync(string username);
    Task<UserProfile> GetUserProfileByContactGuid(Guid contactId);
    Task<UserProfile> GetUserProfileByOfferGuid(Guid offerId);
    // void DeleteTag();
    void Update(UserProfile userProfile);
}