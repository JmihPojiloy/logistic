using Logistics.Application.Interfaces.Services;
using Logistics.Application.Services;
using Logistics.Domain.Entities.Products;
using Logistics.Domain.Entities.Warehouses;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IService<Product>, ProductService>();
        services.AddScoped<IService<Warehouse>, WarehouseService>();
        return services;
    }
}