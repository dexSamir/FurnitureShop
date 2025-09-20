using System.ComponentModel;
using FurnitureShop.BL.Utilities.Enums;

namespace FurnitureShop.BL.Dtos.ProductDtos;

public class ProductFilterDto
{
    public string? Search { get; set; }
    public int? CategoryId { get; set; } 

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    [DefaultValue(1)]
    public int Page { get; set; } = 1;
    [DefaultValue(30)]
    public int PageSize { get; set; } = 30; 
    
    public ESortDirection SortDirection { get; set; } = ESortDirection.ASC;
}