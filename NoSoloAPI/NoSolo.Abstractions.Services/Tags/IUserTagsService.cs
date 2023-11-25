using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Users.Tags;
using NoSolo.Core.Specification.Users.UserTag;

namespace NoSolo.Abstractions.Services.Tags;

public interface IUserTagsService
{
    Task<UserTagDto> Add(NewUserTagDto userTagDto, string email);
    Task<UserTagDto> Update(UpdateUserTagDto userTagDto, string email);
    Task Delete(Guid userTagGuid, string email);

    Task<UserTagDto> Get(Guid userTagGuid);
    Task<Pagination<UserTagDto>> Get(UserTagParams userTagParams);

    Task<UserTagDto> ChangeActiveTask(Guid userTagGuid, string email);
}