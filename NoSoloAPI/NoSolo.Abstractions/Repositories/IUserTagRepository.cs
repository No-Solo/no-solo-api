using NoSolo.Core.Entities;
using NoSolo.Core.Entities.User;

namespace NoSolo.Abstractions.Repositories;

public interface IUserTagRepository
{
    Task<UserTag> GetUserTagByGuid(Guid id);
    void Update(UserTag userTag);
}