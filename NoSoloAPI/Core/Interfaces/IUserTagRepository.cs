using Core.Entities;

namespace Core.Interfaces;

public interface IUserTagRepository
{
    Task<UserTag> GetUserTagByGuid(Guid id);
    void Update(UserTag userTag);
}