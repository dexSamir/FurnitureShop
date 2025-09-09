using FurnitureShop.Core.Entities;
using FurnitureShop.Core.Repositories;
using FurnitureShop.DAL.Context;

namespace FurnitureShop.DAL.Repositories;

public class ProductImageRepository(AppDbContext context) : GenericRepository<ProductImage>(context), IProductImageRepository
{
    
}