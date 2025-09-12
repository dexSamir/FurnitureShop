using FurnitureShop.BL.Dtos.ProductDtos;
using FurnitureShop.BL.Utilities.Enums;

namespace FurnitureShop.BL.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductGetDto>> GetAllAsync(ProductFilterDto dto);
    Task<ProductDetailDto> GetByIdAsync(int id);
    Task<ProductDetailDto> GetByPublicIdAsync(Guid publicId);
    
    Task<ProductDetailDto> CreateAsync(ProductCreateDto dto);
    Task<IEnumerable<ProductGetDto>> CreateBulkAsync(IEnumerable<ProductCreateDto> dtos); 
    Task<ProductDetailDto> UpdateAsync(int id, ProductUpdateDto dto);
    

    Task<bool> DeleteAsync(int[] ids, EDeleteType dType); 
}