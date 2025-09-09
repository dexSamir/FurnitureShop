using FurnitureShop.Core.Entities;
using FurnitureShop.Core.Repositories;
using FurnitureShop.DAL.Context;

namespace FurnitureShop.DAL.Repositories;

public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
{
    
}