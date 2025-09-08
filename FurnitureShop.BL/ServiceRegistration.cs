using FluentValidation;
using FluentValidation.AspNetCore;
using FurnitureShop.BL.OtherServices.Implements;
using FurnitureShop.BL.OtherServices.Interfaces;
using FurnitureShop.BL.Services.Implements;
using FurnitureShop.BL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
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
    
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceRegistration));
        return services;
    }

    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
    
    public static IServiceCollection AddCache(this IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
            options.InstanceName = "FurnitureShop_"; 
        }); 
        return services;
    }
}