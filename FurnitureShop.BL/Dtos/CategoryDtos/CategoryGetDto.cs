namespace FurnitureShop.BL.Dtos.CategoryDtos;

public class CategoryGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsUpdated { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public int? ParentCategoryId { get; set; } 
    public ICollection<CategoryGetDto>? Subcategories { get; set; } 
}