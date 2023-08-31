using Core.Entities;

namespace Core.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByUsernameWithAllIncludesAsync(string username);
    Task<User> GetUserByUsernameWithTagIncludeAsync(string username);
    Task<User> GetUserByUsernameWithPhotoIncludeAsync(string username);
    void Update(User user);
    Task<bool> UserExists(string username);
}