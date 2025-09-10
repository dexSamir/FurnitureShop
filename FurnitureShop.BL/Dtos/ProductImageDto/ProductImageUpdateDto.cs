using Microsoft.AspNetCore.Http;

namespace FurnitureShop.BL.Dtos.ProductImageDto;

public class ProductImageUpdateDto
{
    public string? AltText { get; set; }
    public bool? IsPrimary {get; set;}
    public bool? IsSecondary { get; set; }
    public string? ExistingImageUrl { get; set; }
    public IFormFile? Image { get; set; }
}