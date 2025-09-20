using FurnitureShop.BL.Dtos.ProductImageDto;
using FurnitureShop.BL.Services.Interfaces;
using FurnitureShop.BL.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductImageController(IProductImageService service) : ControllerBase
{
    [HttpGet("/GetImagesByProductId/{productId}")]
    public async Task<IActionResult> GetImagesByProductId(Guid productId)
    {
        return Ok(await service.GetImagesByProductId(productId));
    }

    [HttpGet("/GetByImageId/{imageId}")]
    public async Task<IActionResult> GetById(int imageId)
    {
        return Ok(await service.GetImageById(imageId));
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> AddImages(Guid productId,[FromForm] IList<ProductImageCreateDto> dtos)
    {
        await service.AddImagesAsync(productId, dtos);
        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateImage(int imageId, ProductImageUpdateDto dto)
    {
        return Ok(await service.UpdateImage(imageId, dto));
    }

    [HttpDelete("{deleteType}")]
    public async Task<IActionResult> Delete([FromQuery] int[] ids, [FromQuery] EDeleteType deleteType)
    {
        await service.DeleteImagesAsync(ids, deleteType);
        return Ok();
    }
}
