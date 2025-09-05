using FurnitureShop.BL.OtherServices.Implements;
using FurnitureShop.BL.OtherServices.Interfaces;
using FurnitureShop.BL.Services.Implements;
using FurnitureShop.BL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FurnitureShop.BL;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>(); 
        
        services.AddScoped<ICacheService, CacheService>();
        return services;
    }

    public static IServiceCollection AddCache(this IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "http://localhost:5101";
            options.InstanceName = "FurnitureShop_"; 
        }); 
        return services;
    }
}