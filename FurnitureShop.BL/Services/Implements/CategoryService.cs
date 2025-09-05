using AutoMapper;
using FurnitureShop.BL.Constants;
using FurnitureShop.BL.Dtos.CategoryDtos;
using FurnitureShop.BL.Exceptions.Common;
using FurnitureShop.BL.OtherServices.Interfaces;
using FurnitureShop.BL.Services.Interfaces;
using FurnitureShop.BL.Utilities.Enums;
using FurnitureShop.Core.Entities;
using FurnitureShop.Core.Repositories;

namespace FurnitureShop.BL.Services.Implements;

public class CategoryService(IMapper mapper, ICategoryRepository repo, ICacheService cache) : ICategoryService
{
    public async Task<IEnumerable<CategoryGetDto>> GetAllAsync()
    {
        var categories = await cache.GetOrSetAsync(CacheKeys.Category,async () => await repo.GetAllAsync(includes: "Subcategories"), TimeSpan.FromMinutes(2)); 
        return mapper.Map<IEnumerable<CategoryGetDto>>(categories);
    }

    public async Task<CategoryGetDto> GetByIdAsync(int id)
    {
        var category = await repo.GetByIdAsync(id) ?? throw new NotFoundException<Category>();
        return mapper.Map<CategoryGetDto>(category);
    }

    public async Task<CategoryGetDto> CreateAsync(CategoryCreateDto dto)
    {
        var data = mapper.Map<Category>(dto);
        if(data is null) 
            throw new ArgumentNullException(nameof(data));

        if (dto.ParentCategoryId.HasValue && dto.ParentCategoryId > 0)
            if(!await repo.IsExistAsync(dto.ParentCategoryId.Value)) throw new NotFoundException<Category>();  
        
        await repo.AddAsync(data);
        await repo.SaveAsync();
        
        return mapper.Map<CategoryGetDto>(data);
    }

    public async Task<IEnumerable<CategoryGetDto>> CreateBulkAsync(IEnumerable<CategoryCreateDto> dtos)
    {
        var data = mapper.Map<IEnumerable<Category>>(dtos);
        
        foreach(var dto in dtos) 
            if (dto.ParentCategoryId.HasValue && dto.ParentCategoryId > 0)
                if(!await repo.IsExistAsync(dto.ParentCategoryId.Value)) throw new NotFoundException<Category>();
        
        await repo.AddRangeAsync(data);
        await repo.SaveAsync();
        return mapper.Map<IEnumerable<CategoryGetDto>>(data);
    }

    public async Task<CategoryGetDto> UpdateAsync(int id, CategoryUpdateDto dto)
    {
        var data = await repo.GetByIdAsync(id, false) ?? throw new NotFoundException<Category>();
        
        if (dto.ParentCategoryId.HasValue && dto.ParentCategoryId > 0)
            if(!await repo.IsExistAsync(dto.ParentCategoryId.Value)) throw new NotFoundException<Category>();
        
        mapper.Map(dto, data);
        await repo.UpdateAsync(data); 
        await repo.SaveAsync();
        await cache.RemoveAsync(CacheKeys.Category); 
        return  mapper.Map<CategoryGetDto>(data);
    }

    public async Task<bool> DeleteAsync(int[] ids, EDeleteType dType)
    {
        if(ids.Length == 0) throw new EmptyIdsException<Category>();

        switch (dType)
        {
            case EDeleteType.Soft:
                await repo.SoftDeleteRangeAsync(ids);
                break;

            case EDeleteType.Reverse:
                await repo.ReverseDeleteRangeAsync(ids);
                break;

            case EDeleteType.Hard:
                await repo.HardDeleteRangeAsync(ids);
                break;

            // default:
            //     throw new UnsupportedDeleteTypeException($"Delete type '{dType}' is not supported."); 
            
        }
        await cache.RemoveAsync(CacheKeys.Category); 
        return await repo.SaveAsync() == ids.Length;
    }
}