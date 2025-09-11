using FurnitureShop.BL.Dtos.ProductImageDto;

namespace FurnitureShop.BL.Dtos.ProductDtos;

public class ProductDetailDto
{
    public Guid PublicId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public decimal SellPrice { get; set; }
    public decimal CostPrice { get; set; }
    
    public int Discount  { get; set; }
    public int Quantity { get; set; }
    
    public int CategoryId { get; set; } 
    
    public bool IsDeleted { get; set; }
    public bool IsUpdated { get; set; }
    public DateTime UpdatedTime { get; set; }
    public DateTime CreatedTime { get; set; }
    public IEnumerable<ProductImageGetDto> Images { get; set; }
}