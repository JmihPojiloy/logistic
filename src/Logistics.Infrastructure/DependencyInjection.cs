using Logistics.Application.Interfaces.Geo;
using Logistics.Application.Interfaces.Initializations;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.UnitOfWork;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.ExternalServices.Geo;
using Logistics.Infrastructure.Initializations;
using Logistics.Infrastructure.Mapper;
using Logistics.Infrastructure.Repositories;
using Logistics.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Infrastructure;

/// <summary>
/// Класс с методом расширения для регистрации сервисов слоя в DI
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresDbConnection");

        services.AddDbContext<LogisticDbContext>(options => options.UseNpgsql(connectionString));
        services.AddAutoMapper(typeof(LogisticEntitiesMappingProfile));
        services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
        services.AddScoped<IGeoService, YandexGeoService>();
        services.AddHttpClient();
        
        return services;
    }
}