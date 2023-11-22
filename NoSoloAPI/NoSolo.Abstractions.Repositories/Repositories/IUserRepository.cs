using NoSolo.Core.Entities.User;

namespace NoSolo.Abstractions.Repositories.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByEmailWithWithoutIncludesAsync(string email);
    Task<User> GetUserByEmailWithAllIncludesAsync(string email);
    void Update(User user);
    Task<bool> UserExists(string email);
}