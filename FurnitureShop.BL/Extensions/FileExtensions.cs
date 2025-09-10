using Microsoft.AspNetCore.Http;
namespace FurnitureShop.BL.Extensions;

public static class FileExtensions
{
    public static bool IsValidType(this IFormFile file, string type)
        => file.FileName.StartsWith(type);

    public static bool IsValidImage(this IFormFile file, int mb)
        => file.Length <= mb * 1024 * 1024;

    public static async Task<string> UploadAsync(this IFormFile file, params string[] paths)
    {
        string path = Path.Combine(paths);
        if (!Path.Exists(path))
            Directory.CreateDirectory(path); 
        
        
        string fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
        using (Stream sr = File.Create(Path.Combine(path, fileName)))
            await file.CopyToAsync(sr);
        
        return fileName; 
    }
    
    public static bool DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        return false;
    }
}