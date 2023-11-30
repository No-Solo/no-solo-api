using Microsoft.AspNetCore.Http;
using NoSolo.Abstractions.Services.Photos;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Contracts.Dtos.Users.Photo;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Infrastructure.Services.Photos;

public class UserPhotoService : IUserPhotoService
{
    private readonly IPhotoService _photoService;
    private readonly IUserService _userService;

    private User? _user;

    public UserPhotoService(IPhotoService photoService, IUserService userService)
    {
        _photoService = photoService;
        _userService = userService;
        _user = null;
    }

    public async Task<UserPhotoDto> Add(IFormFile file, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Photo);

        return await _photoService.Add(_user, file);
    }

    public async Task DeleteUserPhoto(string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Photo);

        await _photoService.Delete(_user);
    }

    public async Task<UserPhotoDto> Get(string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Photo);

        return await _photoService.GetMainDto(_user);
    }
}