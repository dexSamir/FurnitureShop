using Microsoft.AspNetCore.Http;

namespace FurnitureShop.BL.Dtos.ProductImageDto;

public class ProductImageCreateDto
{
    public IFormFile Image { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsSecondary { get; set; }
    public string AltText { get; set; }
}