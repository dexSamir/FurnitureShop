using FurnitureShop.BL.Dtos.ProductDtos;
using FurnitureShop.BL.Services.Interfaces;
using FurnitureShop.BL.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService service): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] ProductFilterDto dto)
    {
        return Ok(await service.GetAllAsync(dto));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await service.GetByIdAsync(id));
    }
    
    [HttpGet("{publicId}")]
    public async Task<IActionResult> GetById(Guid publicId)
    {
        return Ok(await service.GetByPublicIdAsync(publicId));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProductCreateDto dto)
    {
        return Ok(await service.CreateAsync(dto)); 
    }

    [HttpPost]
    public async Task<IActionResult> CreateRange(IEnumerable<ProductCreateDto> dtos)
    {
        return Ok(await service.CreateBulkAsync(dtos));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, ProductUpdateDto dto)
    {
        return Ok(await service.UpdateAsync(id, dto));
    }

    [HttpDelete("{deleteType}")]
    public async Task<IActionResult> Delete([FromQuery] int[] ids, EDeleteType deleteType)
    {
        return Ok(await service.DeleteAsync(ids, deleteType));
    }
}
