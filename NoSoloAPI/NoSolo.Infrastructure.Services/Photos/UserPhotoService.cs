using Microsoft.AspNetCore.Http;
using NoSolo.Abstractions.Services.Photos;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Contracts.Dtos.Users.Photo;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Infrastructure.Services.Photos;

public class UserPhotoService(IPhotoService photoService, IUserService userService) : IUserPhotoService
{
    private UserEntity? _user = null;

    public async Task<UserPhotoDto> Add(IFormFile file, string email)
    {
        _user ??= await userService.GetUser(email, UserInclude.Photo);

        return await photoService.Add(_user, file);
    }

    public async Task DeleteUserPhoto(string email)
    {
        _user ??= await userService.GetUser(email, UserInclude.Photo);

        await photoService.Delete(_user);
    }

    public async Task<UserPhotoDto> Get(string email)
    {
        _user ??= await userService.GetUser(email, UserInclude.Photo);

        return await photoService.GetMainDto(_user);
    }
}