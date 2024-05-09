using AutoMapper;
using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Contracts.Dtos.Users;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Users.Users;

namespace NoSolo.Infrastructure.Services.Users;

public class UserService(IRepository<UserEntity> userRepository, IMapper mapper) : IUserService
{
    public async Task<UserDto> Get(string email)
    {
        var user = await GetUser(email, new List<UserInclude>()
        {
            UserInclude.Contacts, UserInclude.Membership, UserInclude.Offers, UserInclude.Photo,
            UserInclude.Requests, UserInclude.Tags
        });

        return mapper.Map<UserDto>(user);
    }

    public async Task<UserEntity> GetUser(string email, List<UserInclude> includes)
    {
        var userParams = new UserParams()
        {
            Email = email,
            Includes = includes
        };

        return await GetUserBySpecification(userParams);
    }

    public async Task<UserEntity> GetUser(string email, UserInclude include)
    {
        var userParams = new UserParams()
        {
            Email = email,
            Includes = new List<UserInclude>() { include }
        };

        return await GetUserBySpecification(userParams);
    }

    public async Task<UserDto> Update(UpdateUserDto updateUserDto, string email)
    {
        var user = await GetUser(email,
            new List<UserInclude>()
            {
                UserInclude.Contacts, UserInclude.Membership, UserInclude.Offers, UserInclude.Photo,
                UserInclude.Requests, UserInclude.Tags
            });

        mapper.Map(updateUserDto, user);
        userRepository.Save();

        return mapper.Map<UserDto>(user);
    }

    private async Task<UserEntity> GetUserBySpecification(UserParams userParams)
    {
        var spec = new UserWithSpecificationParams(userParams);

        var user = await userRepository.GetEntityWithSpec(spec);
        if (user is null)
            throw new EntityNotFound("The userEntity is not found");

        return user;
    }
}