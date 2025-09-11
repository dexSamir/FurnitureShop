namespace FurnitureShop.BL.Dtos.ProductDtos;

public class ProductCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    
    public decimal SellPrice { get; set; }
    public decimal CostPrice { get; set; }
    
    public int Discount  { get; set; }
    public int Quantity { get; set; }
    
    public int CategoryId { get; set; } 
}