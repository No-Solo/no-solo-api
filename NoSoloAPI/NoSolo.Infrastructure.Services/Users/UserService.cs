using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Users.Users;

namespace NoSolo.Infrastructure.Services.Users;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> GetUser(string email, List<UserInclude> includes)
    {
        var userParams = new UserParams()
        {
            Email = email,
            Includes = includes
        };

        var user = await GetUserBySpecification(userParams);
        if (user is null)
            throw new EntityNotFound("The user is not found");

        return user;
    }

    public async Task<User> GetUser(string email, UserInclude include)
    {
        var userParams = new UserParams()
        {
            Email = email,
            Includes = new List<UserInclude>() { include }
        };
        
        var user = await GetUserBySpecification(userParams);
        if (user is null)
            throw new EntityNotFound("The user is not found");

        return user;
    }

    private async Task<User> GetUserBySpecification(UserParams userParams)
    {
        var spec = new UserWithSpecificationParams(userParams);

        return await _unitOfWork.Repository<User>().GetEntityWithSpec(spec);
    }
}