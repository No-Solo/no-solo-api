using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Repositories.Repositories;
using NoSolo.Core.Entities.User;
using NoSolo.Infrastructure.Data.Data;

namespace NoSolo.Infrastructure.Repositories.Users;

public class UserTagRepository : IUserTagRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public UserTagRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    
    public async Task<UserTag> GetUserTagByGuid(Guid id)
    {
        return await _dataBaseContext.UserTags.SingleOrDefaultAsync(x => x.Id == id);
    }

    public void Update(UserTag userTag)
    {
        _dataBaseContext.Entry(userTag).State = EntityState.Modified;
    }
}