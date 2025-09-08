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
        var categories = await cache.GetOrSetAsync(
            CacheKeys.Category,
            async () => await repo.GetAllAsync(),
            TimeSpan.FromMinutes(2)
        );

        var rootCategories = categories.Where(x => x.ParentCategoryId == null).ToList();

        return rootCategories.Select(x => MapCategory(x, categories)).ToList();
    }

    public async Task<CategoryGetDto> GetByIdAsync(int id)
    {
        var allCategories = await repo.GetAllAsync();

        var category = allCategories.FirstOrDefault(x => x.Id == id);
        if (category == null)
            throw new NotFoundException<Category>();

        return MapCategory(category, allCategories);
    }
    
    private CategoryGetDto MapCategory(Category category, IEnumerable<Category> allCategories)
    {
        return new CategoryGetDto
        {
            Id = category.Id,
            Name = category.Name,
            ParentCategoryId = category.ParentCategoryId,
            IsDeleted = category.IsDeleted,
            UpdatedTime = category.UpdatedTime,
            IsUpdated = category.IsUpdated,
            Subcategories = allCategories
                .Where(x => x.ParentCategoryId == category.Id)
                .Select(x => MapCategory(x, allCategories))
                .ToList()
        };
    }

    public async Task<CategoryGetDto> CreateAsync(CategoryCreateDto dto)
    {
        if(dto.IsPrimary) 
            dto.ParentCategoryId = null;
         
        if (dto.ParentCategoryId is not null)
            if (!await repo.IsExistAsync(dto.ParentCategoryId.Value))
                throw new NotFoundException<Category>();
        
        var data = mapper.Map<Category>(dto); 
        
        await repo.AddAsync(data);
        await repo.SaveAsync();
        await cache.RemoveAsync(CacheKeys.Category);
        return mapper.Map<CategoryGetDto>(data);
    }

    public async Task<IEnumerable<CategoryGetDto>> CreateBulkAsync(IEnumerable<CategoryCreateDto> dtos)
    {
        var dtoList = dtos.ToList();

        foreach (var dto in dtoList)
            if (dto.IsPrimary)
                dto.ParentCategoryId = null;

        int[] parentIds = dtoList
            .Where(x => x.ParentCategoryId.HasValue)
            .Select(x => x.ParentCategoryId!.Value)
            .Distinct()
            .ToArray();

        if (parentIds.Length > 0 && !await repo.IsExistRangeAsync(parentIds))
            throw new NotFoundException<Category>();

        var entities = mapper.Map<IEnumerable<Category>>(dtoList);

        await repo.AddRangeAsync(entities);
        await repo.SaveAsync();

        return mapper.Map<IEnumerable<CategoryGetDto>>(entities);
    }


    public async Task<CategoryGetDto> UpdateAsync(int id, CategoryUpdateDto dto)
    {
        if(dto.IsPrimary == true) 
            dto.ParentCategoryId = null;
        
        if (dto.ParentCategoryId.HasValue && dto.ParentCategoryId > 0)
            if(!await repo.IsExistAsync(dto.ParentCategoryId.Value)) throw new NotFoundException<Category>();
        
        var data = await repo.GetByIdAsync(id, false, includes:"Subcategories") ?? throw new NotFoundException<Category>();
        
        
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