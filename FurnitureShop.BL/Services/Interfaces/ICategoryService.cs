using FurnitureShop.BL.Dtos.CategoryDtos;
using FurnitureShop.BL.Utilities.Enums;

namespace FurnitureShop.BL.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryGetDto>> GetAllAsync(); 
    Task<CategoryGetDto> GetByIdAsync(int id);
    Task<CategoryGetDto> CreateAsync(CategoryCreateDto dto);
    Task<IEnumerable<CategoryGetDto>> CreateBulkAsync(IEnumerable<CategoryCreateDto> dtos);
    Task<CategoryGetDto> UpdateAsync(int id, CategoryUpdateDto dto);
    Task<bool> DeleteAsync(int[] ids, EDeleteType dType); 
}