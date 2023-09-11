using Core.Entities;

namespace Core.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByUsernameWithMembersIncludeAsync(string username);
    Task<User> GetUserByUsernameWithWithoutIncludesAsync(string username);
    Task<User> GetUserByUsernameWithAllIncludesAsync(string username);
    Task<User> GetUserByUsernameWithTagIncludeAsync(string username);
    Task<User> GetUserByUsernameWithPhotoIncludeAsync(string username);
    Task<User> GetUserByUsernameWithOrganization(string username);
    void Update(User user);
    Task<bool> UserExists(string username);
}