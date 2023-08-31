using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public UserProfileRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    
    public async Task<UserProfile> GetUserProfileByUsernameWithAllIncludesAsync(string username)
    {
        return await _dataBaseContext.UserProfiles
            .Include(x => x.Photo)
            .Include(x => x.Tags)
            .Include(x => x.Contacts)
            .Include(x => x.Offers)
            .SingleOrDefaultAsync(x => x.User.UserName.ToLower() == username.ToLower());
    }

    public async Task<UserProfile> GetUserProfileByUsernameWithTagsIncludeAsync(string username)
    {
        return await _dataBaseContext.UserProfiles
            .Include(x => x.Tags)
            .SingleOrDefaultAsync(x => x.User.UserName.ToLower() == username.ToLower());
    }

    public async Task<UserProfile> GetUserProfileByUsernameWithPhotoIncludeAsync(string username)
    {
        return await _dataBaseContext.UserProfiles
            .Include(x => x.Photo)
            .SingleOrDefaultAsync(x => x.User.UserName.ToLower() == username.ToLower());
    }

    public async Task<UserProfile> GetUserProfileByUsernameWithContactsIncludeAsync(string username)
    {
        return await _dataBaseContext.UserProfiles
            .Include(x => x.Contacts)
            .SingleOrDefaultAsync(x => x.User.UserName.ToLower() == username.ToLower());
    }

    public async Task<UserProfile> GetUserProfileByUsernameWithOffersIncludeAsync(string username)
    {
        return await _dataBaseContext.UserProfiles
            .Include(x => x.Offers)
            .SingleOrDefaultAsync(x => string.Equals(x.User.UserName, username, StringComparison.CurrentCultureIgnoreCase));
    }

    public void Update(UserProfile userProfile)
    {
        _dataBaseContext.Entry(userProfile).State = EntityState.Modified;
    }
}