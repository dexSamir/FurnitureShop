using FurnitureShop.BL.Exceptions.Image;
using FurnitureShop.BL.Extensions;
using FurnitureShop.BL.OtherServices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FurnitureShop.BL.OtherServices.Implements;

public class FileService : IFileService
{
    public async Task DeleteImageIfNotDefault(string imageUrl, string folder)
    {
        string defaultImage = $"/imgs/{folder}/default.jpg";
        if (!string.IsNullOrEmpty(imageUrl) && imageUrl != defaultImage)
        {
            string fileName = Path.Combine("wwwroot", "imgs", folder, imageUrl);
            FileExtensions.DeleteFile(fileName); 
        }
    }

    public async Task<string> ProcessImageAsync(IFormFile file, string directory, string fileType, int maxSize, string? existingFilePath = null)
    {
        if (file == null)
            return existingFilePath;

        if (!file.IsValidType("image"))
            throw new UnsupportedFileTypeException($"File must be of type {fileType}!");

        if (!file.IsValidSize(maxSize))
            throw new UnsupportedFileSizeException($"File size must be less than {maxSize}MB!");

        if(!string.IsNullOrEmpty(existingFilePath))
            FileExtensions.DeleteFile(Path.Combine("wwwroot", directory, existingFilePath));

        return await file.UploadAsync("wwwroot", "imgs", directory);
    }
}