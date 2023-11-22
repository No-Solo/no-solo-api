using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace NoSolo.Abstractions.Services.Photos;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}