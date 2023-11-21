using NoSolo.Core.Entities.User;

namespace NoSolo.Abstractions.Repositories.Repositories;

public interface IUserTagRepository
{
    Task<UserTag> GetUserTagByGuid(Guid id);
    void Update(UserTag userTag);
}