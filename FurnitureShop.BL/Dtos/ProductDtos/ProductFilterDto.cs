using FurnitureShop.BL.Utilities.Enums;

namespace FurnitureShop.BL.Dtos.ProductDtos;

public class ProductFilterDto
{
    public string? Search { get; set; }
    public int? CategoryId { get; set; }

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public int Page { get; set; }
    public int PageSize { get; set; }

    public ESortDirection SortDirection { get; set; } = ESortDirection.ASC;
}