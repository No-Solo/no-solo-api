using NoSolo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Repositories;
using NoSolo.Core.Entities.User;
using NoSolo.Infrastructure.Data;

namespace NoSolo.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public UserRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<User> GetUserByUsernameWithMembersIncludeAsync(string username)
    {
        return await _dataBaseContext.Users
            .Include(x => x.OrganizationUsers)
            .SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
    }

    public async Task<User> GetUserByUsernameWithWithoutIncludesAsync(string username)
    {
        return await _dataBaseContext.Users
            .SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
    }

    public async Task<User> GetUserByUsernameWithAllIncludesAsync(string username)
    {
        return await _dataBaseContext.Users
            .Include(x => x.UserProfile)
            .Include(x => x.UserProfile.Photo)
            .Include(x => x.UserProfile.Tags)
            .Include(x => x.OrganizationUsers)
            .SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
    }

    public async Task<User> GetUserByUsernameWithTagIncludeAsync(string username)
    {
        return await _dataBaseContext.Users
            .Include(x => x.UserProfile)
            .Include(x => x.UserProfile.Tags)
            .SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
    }

    public async Task<User> GetUserByUsernameWithPhotoIncludeAsync(string username)
    {
        return await _dataBaseContext.Users
            .Include(x => x.UserProfile)
            .Include(x => x.UserProfile.Photo)
            .SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
    }

    public async Task<User> GetUserByUsernameWithOrganization(string username)
    {
        return await _dataBaseContext.Users
            .Include(x => x.OrganizationUsers)
            .SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
    }

    public async Task<User> GetUserByGuidWithMembersIncludeAsync(Guid guid)
    {
        return await _dataBaseContext.Users
            .Include(x => x.OrganizationUsers)
            .SingleOrDefaultAsync(x => x.Id == guid);
    }

    public async Task<bool> UserExists(string username)
    {
        return await _dataBaseContext.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
    
    public void Update(User user)
    {
        _dataBaseContext.Entry(user).State = EntityState.Modified;
    }
}