namespace FurnitureShop.BL.Dtos.CategoryDtos;

public class CategoryUpdateDto
{
    public string? Name {get; set; }
    public bool? IsPrimary { get; set; }
    public int? ParentCategoryId { get; set; }
}