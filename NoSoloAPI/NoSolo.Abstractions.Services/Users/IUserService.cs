using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Abstractions.Services.Users;

public interface IUserService
{
    Task<User> GetUser(string email, List<UserInclude> includes);
    Task<User> GetUser(string email, UserInclude include);
}