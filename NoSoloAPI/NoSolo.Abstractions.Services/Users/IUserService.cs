using NoSolo.Contracts.Dtos.Users;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Abstractions.Services.Users;

public interface IUserService
{
    Task<UserDto> Get(string email);
    Task<UserEntity> GetUser(string email, List<UserInclude> includes);
    Task<UserEntity> GetUser(string email, UserInclude include);

    Task<UserDto> Update(UpdateUserDto updateUserDto, string email);
}