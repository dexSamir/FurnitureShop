using FurnitureShop.Core.Repositories;
using FurnitureShop.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FurnitureShop.DAL;

public static class RepositoryRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>(); 
        
        return services; 
    }
}