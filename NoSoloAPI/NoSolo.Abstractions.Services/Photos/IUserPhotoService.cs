using Microsoft.AspNetCore.Http;
using NoSolo.Contracts.Dtos.Users.Photo;

namespace NoSolo.Abstractions.Services.Photos;

public interface IUserPhotoService
{
    Task<UserPhotoDto> Add(IFormFile file, string email);
    Task<UserPhotoDto> Get(string email);
    Task DeleteUserPhoto(string email);
}