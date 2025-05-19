using System.Text;
using Logistics.Application.Interfaces.Geo;
using Logistics.Application.Interfaces.Initializations;
using Logistics.Application.Interfaces.Payments;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.Security;
using Logistics.Application.Interfaces.Services;
using Logistics.Application.Interfaces.UnitOfWork;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.ExternalServices.Geo;
using Logistics.Infrastructure.ExternalServices.Payments;
using Logistics.Infrastructure.Initializations;
using Logistics.Infrastructure.Mapper;
using Logistics.Infrastructure.Repositories;
using Logistics.Infrastructure.Repositories.Users;
using Logistics.Infrastructure.Security;
using Logistics.Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
        services.AddScoped<IGeoService, YandexGeoService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IUserCredentialRepository, UserCredentialRepository>();
        services.AddHttpClient();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        
        var jwtSettings = configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"] ?? string.Empty);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        services.AddAuthorization();
        
        return services;
    }
}