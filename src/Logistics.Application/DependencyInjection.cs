using Logistics.Application.Interfaces.Payments;
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
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}