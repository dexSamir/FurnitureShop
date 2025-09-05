namespace FurnitureShop.BL.Dtos.CategoryDtos;

public class CategoryUpdateDto
{
    public string? Name {get; set; }
    public bool? IsPrimary { get; set; }
    public int? ParentId { get; set; }
    public ICollection<CategoryGetDto>? Children { get; set; }
}