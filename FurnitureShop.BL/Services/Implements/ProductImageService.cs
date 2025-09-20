using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FurnitureShop.BL.Constants;
using FurnitureShop.BL.Dtos.ProductImageDto;
using FurnitureShop.BL.Exceptions.Common;
using FurnitureShop.BL.Exceptions.Image;
using FurnitureShop.BL.OtherServices.Interfaces;
using FurnitureShop.BL.Services.Interfaces;
using FurnitureShop.BL.Utilities.Enums;
using FurnitureShop.Core.Entities;
using FurnitureShop.Core.Repositories;

namespace FurnitureShop.BL.Services.Implements;

public class ProductImageService(ICacheService cache, IProductImageRepository repo, IFileService fileService, IMapper mapper, IProductRepository productRepo) : IProductImageService
{
    public async Task<IEnumerable<ProductImageGetDto>> GetImagesByProductId(Guid productId)
    {
        return mapper.Map<IEnumerable<ProductImageGetDto>>(await cache.GetOrSetAsync(CacheKeys.ProductImage, async () => await repo.GetFirstAsync(x=> x.Product.PublicId == productId), TimeSpan.FromMinutes(2)));
    }
    
    public async Task<ProductImageGetDto?> GetImageById(int id)
    {
        var data = await repo.GetByIdAsync(id) ?? throw new NotFoundException<ProductImage>("Product Image is not found!"); 
        return mapper.Map<ProductImageGetDto>(await repo.GetByIdAsync(id)); 
    }
    
    public async Task AddImagesAsync(Guid productId, IList<ProductImageCreateDto> dtos)
    {
        if (!await repo.IsExistAsync(x=> x.Product!.PublicId == productId))
            throw new NotFoundException<Product>();

        MarkPrimaryAndSecondary(dtos);

        List<ProductImage> images = new(); 

        for (int i = 0; i < dtos.Count; i++)
        {
            var data = mapper.Map<ProductImage>(dtos[i]);
            data.ImageUrl = await fileService.ProcessImageAsync(dtos[i].Image, "productImages", "image/", 15);
            data.CreatedTime = DateTime.UtcNow;
            images.Add(data);
        }

        await repo.AddRangeAsync(images);
        await repo.SaveAsync();
        await cache.RemoveAsync(CacheKeys.ProductImage); 
    }

    public async Task<ProductImageGetDto> UpdateImage(int imageId, ProductImageUpdateDto dto)
    {
        var data = await repo.GetByIdAsync(imageId, false) ?? throw new NotFoundException<ProductImage>("Image is not found!"); 
        
        if (dto.Image != null)
        {
            var newFilePath = await fileService.ProcessImageAsync(dto.Image,  "productImages", "image/", 15, dto.ExistingImageUrl);
            data.ImageUrl = newFilePath;
        }
        
        mapper.Map(dto, data);
        
        await repo.UpdateAsync(data);
        await repo.SaveAsync();
        await cache.RemoveAsync(CacheKeys.ProductImage);
        return mapper.Map<ProductImageGetDto>(data);
    }

    public async Task DeleteImagesAsync(int[] imageId, EDeleteType dType)
    {
        if (dType == EDeleteType.Hard)
            foreach (var id in imageId)
            {
                var data = await repo.GetByIdAsync(id, false) ?? throw new NotFoundException<ProductImage>();

                if (!string.IsNullOrEmpty(data.ImageUrl))
                    await fileService.DeleteImageIfNotDefault(data.ImageUrl, "ProductImages");
            }

        switch (dType)
        {
            case EDeleteType.Soft:
                await repo.SoftDeleteRangeAsync(imageId);
                break;

            case EDeleteType.Reverse:
                await repo.ReverseDeleteRangeAsync(imageId);
                break;

            case EDeleteType.Hard:
                await repo.HardDeleteRangeAsync(imageId);
                break;

            default:
                throw new UnsupportedFileTypeException($"Delete type '{dType}' is not supported.");
        }
        
        await cache.RemoveAsync(CacheKeys.ProductImage);
        await repo.SaveAsync();
    }
    
    
    // private methods
    private void MarkPrimaryAndSecondary(IList<ProductImageCreateDto> dtos)
    {
        if (dtos.Count(x => x.IsPrimary) > 1 || dtos.Count(x => x.IsSecondary) > 1)
            throw new ValidationException("Only one image can be primary.");

        if (dtos.Any(x => x.IsPrimary && x.IsSecondary))
            throw new ValidationException("An image cannot be both primary and secondary.");

        var primary = dtos.FirstOrDefault(x => x.IsPrimary); 
        var secondary = dtos.FirstOrDefault(x => x.IsSecondary);
        foreach (var dto in dtos)
        {
            dto.IsPrimary = dto == primary;
            dto.IsSecondary = dto == secondary;
        }
    }
}