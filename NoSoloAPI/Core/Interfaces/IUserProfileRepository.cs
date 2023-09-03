using Core.Entities;

namespace Core.Interfaces;

public interface IUserProfileRepository
{
    Task<UserProfile> GetUserProfileByUsernameWithAllIncludesAsync(string username);
    Task<UserProfile> GetUserProfileByUsernameWithTagsIncludeAsync(string username);
    Task<UserProfile> GetUserProfileByUsernameWithPhotoIncludeAsync(string username);
    Task<UserProfile> GetUserProfileByUsernameWithContactsIncludeAsync(string username);
    Task<UserProfile> GetUserProfileByUsernameWithOffersIncludeAsync(string username);
    Task<UserProfile> GetUserProfileByContactGuid(Contact<UserProfile> contact);
    // void DeleteTag();
    void Update(UserProfile userProfile);
}