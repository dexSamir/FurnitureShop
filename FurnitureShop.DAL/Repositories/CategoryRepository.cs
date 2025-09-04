using FurnitureShop.Core.Entities;
using FurnitureShop.Core.Repositories;
using FurnitureShop.DAL.Context;

namespace FurnitureShop.DAL.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
            
    }
}