using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace NoSolo.Abstractions.Services.Photos;

public interface ICloudinaryService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}