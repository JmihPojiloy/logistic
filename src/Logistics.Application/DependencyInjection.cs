using Logistics.Application.Interfaces.Services;
using Logistics.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}