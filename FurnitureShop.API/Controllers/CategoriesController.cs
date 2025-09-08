using FurnitureShop.BL.Dtos.CategoryDtos;
using FurnitureShop.BL.Services.Interfaces;
using FurnitureShop.BL.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CategoriesController(ICategoryService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await service.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
    {
        return Ok(await service.CreateAsync(dto));
    }

    [HttpPost]
    public async Task<IActionResult> CreateRange( IEnumerable<CategoryCreateDto> dtos)
    {
        return Ok(await service.CreateBulkAsync(dtos)); 
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id,  [FromBody] CategoryUpdateDto dto)
    {
        return Ok(await service.UpdateAsync(id, dto)); 
    }

    [HttpDelete("{dType}")]
    public async Task<IActionResult> Delete([FromQuery] int[] ids, EDeleteType dType)
    {
        return Ok(await service.DeleteAsync(ids, dType));
    }
}