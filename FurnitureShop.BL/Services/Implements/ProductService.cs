using System.Linq.Expressions;
using AutoMapper;
using FurnitureShop.BL.Dtos.ProductDtos;
using FurnitureShop.BL.Exceptions.Common;
using FurnitureShop.BL.Services.Interfaces;
using FurnitureShop.BL.Utilities.Enums;
using FurnitureShop.Core.Entities;
using FurnitureShop.Core.Repositories;

namespace FurnitureShop.BL.Services.Implements;

public class ProductService(IProductRepository repo, IMapper mapper) : IProductService
{
    public async Task<IEnumerable<ProductGetDto>> GetAllAsync(ProductFilterDto dto)
    {
        Expression<Func<Product, bool>> filter = x =>
            !x.IsDeleted &&
            (!dto.MinPrice.HasValue || x.SellPrice >= dto.MinPrice.Value) &&
            (!dto.MaxPrice.HasValue || x.SellPrice <= dto.MaxPrice.Value) &&
            (!dto.CategoryId.HasValue || x.CategoryId == dto.CategoryId.Value) &&
            (string.IsNullOrEmpty(dto.Search) || x.Title.ToLower().Contains(dto.Search.ToLower()));

        Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = dto.SortDirection switch
        {
            ESortDirection.ASC => q => q.OrderBy(x => x.SellPrice),
            ESortDirection.DESC => q => q.OrderByDescending(x => x.SellPrice),
            _ => null
        };

        var products = await repo.GetPagedAsync(
            filter,
            dto.Page,
            true,
            dto.PageSize,
            orderBy,
            "Categories", "Images"); 
        
        return mapper.Map<IEnumerable<ProductGetDto>>(products);
    }

    public async Task<ProductDetailDto> GetByIdAsync(int id)
    {
        var data = await repo.GetByIdAsync(id, true, "Categories", "Images") ?? 
                   throw new NotFoundException<Product>();
        return mapper.Map<ProductDetailDto>(data); 
    }

    public async Task<ProductDetailDto> GetByPublicIdAsync(Guid publicId)
    {
        var data = await repo.GetFirstAsync(x=> x.PublicId == publicId, true, "Categories", "Images") ?? 
                   throw new NotFoundException<Product>();
        
        return mapper.Map<ProductDetailDto>(data); 
    }

    public async Task<ProductDetailDto> CreateAsync(ProductCreateDto dto)
    {
        var data = mapper.Map<Product>(dto);
        data.CreatedTime = DateTime.UtcNow;

        await repo.AddAsync(data);
        await repo.SaveAsync();
        return mapper.Map<ProductDetailDto>(data); 
    }

    public async Task<IEnumerable<ProductGetDto>> CreateBulkAsync(IEnumerable<ProductCreateDto> dtos)
    {
        var dtoList = dtos.ToList();
        var data = mapper.Map<IList<Product>>(dtoList);

        for (int i = 0; i < data.Count; i++)
            data[i].CreatedTime = DateTime.UtcNow;

        await repo.AddRangeAsync(data);
        await repo.SaveAsync();
        return mapper.Map<IEnumerable<ProductGetDto>>(data);
    }

    public async Task<ProductDetailDto> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var existing = await repo.GetByIdAsync(id, false) ?? throw new NotFoundException<Product>();

        mapper.Map(dto, existing);
        existing.UpdatedTime = DateTime.UtcNow;

        await repo.UpdateAsync(existing);
        await repo.SaveAsync();

        return mapper.Map<ProductDetailDto>(existing);
    }

    public async Task<bool> DeleteAsync(int[] ids, EDeleteType dType)
    {
        if (ids.Length == 0)
            throw new ArgumentException("Hec bir id daxil edilmeyib!");

        var existingIds = (await repo.GetByIdsAsync(ids, false))
            .Select(x => x.Id)
            .ToArray();
        
        var missingIds = ids.Except(existingIds).ToArray();

        if (missingIds.Any())
            throw new NotFoundException<Product>($"Products not found with ids: {string.Join(",", missingIds)}");

        
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

            default:
                throw new UnsupportedDeleteTypeException($"Delete type '{dType}' is not supported.");
        }

        bool success = await repo.SaveAsync() == ids.Length;

        return success;
    }
}