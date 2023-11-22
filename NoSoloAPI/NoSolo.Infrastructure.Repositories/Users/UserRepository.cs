using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Repositories.Repositories;
using NoSolo.Core.Entities.User;
using NoSolo.Infrastructure.Data.Data;

namespace NoSolo.Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public UserRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<User> GetUserByEmailWithWithoutIncludesAsync(string email)
    {
        return await _dataBaseContext.Users
            .SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
    }

    public async Task<User> GetUserByEmailWithAllIncludesAsync(string email)
    {
        return await _dataBaseContext.Users
            .Include(x => x.UserProfile)
            .Include(x => x.UserProfile.Photo)
            .Include(x => x.UserProfile.Tags)
            .Include(x => x.OrganizationUsers)
            .SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
    }

    public async Task<bool> UserExists(string email)
    {
        return await _dataBaseContext.Users.AnyAsync(x => x.Email == email.ToLower());
    }
    
    public void Update(User user)
    {
        _dataBaseContext.Entry(user).State = EntityState.Modified;
    }
}