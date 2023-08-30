using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public UserRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _dataBaseContext.Users
            .Include(x => x.UserProfile)
            .Include(x => x.OrganizationUsers)
            .SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
    }

    public async Task<bool> UserExists(string username)
    {
        return await _dataBaseContext.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}