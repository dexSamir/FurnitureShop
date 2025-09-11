using FurnitureShop.BL.Dtos.ProductImageDto;

namespace FurnitureShop.BL.Dtos.ProductDtos;

public class ProductGetDto
{
    public Guid PublicId { get; set; }
    public string Title { get; set; }
    public decimal SellPrice { get; set; }
    public int Discount  { get; set; }
    public int CategoryId { get; set; } 
    public bool IsDeleted { get; set; }
    public DateTime CreatedTime { get; set; }
    public IEnumerable<ProductImageGetDto> Images { get; set; }
}