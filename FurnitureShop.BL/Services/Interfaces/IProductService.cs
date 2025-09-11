using FurnitureShop.BL.Dtos.ProductDtos;
using FurnitureShop.BL.Utilities.Enums;

namespace FurnitureShop.BL.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductGetDto>> GetAllAsync(ProductFilterDto filter);
    Task<ProductGetDto> GetByIdAsync(int id);
    Task<ProductDetailDto> GetByIdAsync(Guid publicId);
    
    Task<ProductDetailDto> CreateAsync(ProductCreateDto dto);
    Task<IEnumerable<ProductGetDto>> CreateBulkAsync(IEnumerable<ProductCreateDto> dtos); 
    Task<ProductDetailDto> UpdateAsync(ProductUpdateDto dto);

    Task<bool> DeleteAsync(int[] ids, EDeleteType dType); 
}