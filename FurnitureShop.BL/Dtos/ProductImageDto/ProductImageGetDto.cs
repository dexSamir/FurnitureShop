namespace FurnitureShop.BL.Dtos.ProductImageDto;

public class ProductImageGetDto
{
    public int Id { get; set; }
    public int ProductId { get; set; } 
    
    public string ImageUrl { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsSecondary { get; set; }
    public string AltText { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedTime { get; set; }
    
    public bool IsDeleted { get; set; }
    public bool IsUpdated { get; set; }
    
}