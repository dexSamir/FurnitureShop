using FurnitureShop.Core.Entities.Base;

namespace FurnitureShop.Core.Entities;

public class ProductImage : BaseEntity
{
    public string? ImageUrl { get; set; } 
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    
    public bool IsPrimary { get; set; }
    public bool IsSecondary { get; set; }
    
    public string AltText { get; set; }
}