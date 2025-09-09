using FurnitureShop.Core.Entities.Base;

namespace FurnitureShop.Core.Entities;

public class Product : BaseEntity
{
    public Guid PublicId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public decimal SellPrice { get; set; }
    public decimal CostPrice { get; set; }
    
    public int Discount  { get; set; }
    public int Quantity { get; set; }
    
    public int CategoryId { get; set; } 
    public Category Category { get; set; }
    
    public IEnumerable<ProductImage> Images { get; set; }
}