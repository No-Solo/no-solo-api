using Core.Entities;

namespace Core.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByUsernameWithIncludesAsync(string username);
    void Update(User user);
    Task<bool> UserExists(string username);
}