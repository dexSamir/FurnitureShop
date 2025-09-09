using FurnitureShop.Core.Entities.Base;

namespace FurnitureShop.Core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!; 
    public ICollection<Category>? Subcategories { get; set; } 
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; } 
    
    public ICollection<Product>? Products { get; set; } 
}