using Microsoft.AspNetCore.Http;

namespace FurnitureShop.BL.OtherServices.Interfaces;

public interface IFileService
{
    Task DeleteImageIfNotDefault(string imageUrl, string folder);

    Task<string> ProcessImageAsync(IFormFile file, string directory, string fileType, int maxSize,
        string? existingFilePath = null); 
}