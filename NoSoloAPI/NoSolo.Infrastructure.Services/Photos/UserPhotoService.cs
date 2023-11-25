using AutoMapper;
using Microsoft.AspNetCore.Http;
using NoSolo.Abstractions.Services.Photos;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Contracts.Dtos.Users.Photo;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;

namespace NoSolo.Infrastructure.Services.Photos;

public class UserPhotoService : IUserPhotoService
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    private User? _user;
    
    public UserPhotoService(ICloudinaryService cloudinaryService, IUserService userService, IMapper mapper)
    {
        _cloudinaryService = cloudinaryService;
        _userService = userService;
        _mapper = mapper;
        _user = null;
    }
    
    public async Task<UserPhotoDto> Add(IFormFile file, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Photo);

        if (_user.Photo is not null)
            throw new PhotoException("You already have a photo");

        var result = await _cloudinaryService.AddPhotoAsync(file);

        if (result.Error is not null)
            throw new BadRequestException(result.Error.Message);

        var photo = new UserPhoto()
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId,
            User = _user,
            UserGuid = _user.Id
        };

        _user.Photo = photo;
        
        return _mapper.Map<UserPhotoDto>(photo);
    }

    public async Task DeleteUserPhoto(string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Photo);
        
        if (_user.Photo is null)
            throw new PhotoException("You don't have a photo");

        _user.Photo = null;
    }

    public async Task<UserPhotoDto> Get(string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Photo);

        if (_user.Photo is null)
            throw new PhotoException("You don't have a photo");

        return _mapper.Map<UserPhotoDto>(_user.Photo);
    }
}