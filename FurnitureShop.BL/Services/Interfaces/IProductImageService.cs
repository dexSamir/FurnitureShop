using FurnitureShop.BL.Dtos.ProductImageDto;
using FurnitureShop.BL.Utilities.Enums;

namespace FurnitureShop.BL.Services.Interfaces;

public interface IProductImageService
{
    Task AddImagesAsync(int productId, IList<ProductImageCreateDto> dtos); 
    Task<IEnumerable<ProductImageGetDto>> GetImagesByProductId(int productId);
    Task<ProductImageGetDto?> GetImageById( int id);
    Task<ProductImageGetDto> UpdateImage(int imageId, ProductImageUpdateDto dto);
    Task DeleteImagesAsync(int[] imageId, EDeleteType dType);
}