using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Repositories.Repositories;
using NoSolo.Core.Entities.User;
using NoSolo.Infrastructure.Data.Data;

namespace NoSolo.Infrastructure.Repositories.Users;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public UserProfileRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<UserProfile> GetUserProfileWithoutIncludesAsync(string username)
    {
        return await _dataBaseContext.UserProfiles.SingleOrDefaultAsync(x =>
            x.User.UserName.ToLower() == username.ToLower());
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
            .SingleOrDefaultAsync(x => x.User.UserName.ToLower() == username.ToLower());
    }

    public async Task<UserProfile> GetUserProfileByContactGuid(Guid contactId)
    {
        return await _dataBaseContext.UserProfiles.FirstOrDefaultAsync(x => x.Contacts.Any(y => y.Id == contactId));
    }

    public async Task<UserProfile> GetUserProfileByOfferGuid(Guid offerId)
    {
        return await _dataBaseContext.UserProfiles.FirstOrDefaultAsync(x => x.Offers.Any(y => y.Id == offerId));
    }

    public void Update(UserProfile userProfile)
    {
        _dataBaseContext.Entry(userProfile).State = EntityState.Modified;
    }
}