using Logistics.Application.Interfaces.Initializations;
using Logistics.Domain.Enums;
using Logistics.Domain.ValueObjects;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Logistics.Infrastructure.Initializations;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly LogisticDbContext _context;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(LogisticDbContext context, ILogger<DatabaseInitializer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting migration process...");
        try
        {
            await _context.Database.MigrateAsync(cancellationToken);
            _logger.LogInformation("Database migration completed successfully.");

            if (!_context.Products.Any())
            {
                _logger.LogInformation("No products found. Seeding initial data...");

                var products = new List<ProductEntity>
                {
                    new()
                    {
                        Name = "Смартфон Xiaomi Redmi A3x 3/64GB Midnight Black",
                        Description = "Новый, без дефектов, отличное состояние",
                        Price = new Money(6990m, Currency.RUB),
                        Weight = 0.2,
                        Height = 17,
                        Width = 8,
                        Code = 760000
                    },
                    new()
                    {
                        Name = "Смартфон Apple iPhone 14 128GB MPU93CH/A Black",
                        Description = "Новый, без дефектов, отличное состояние",
                        Price = new Money(54990m, Currency.RUB),
                        Weight = 0.17,
                        Height = 15,
                        Width = 7,
                        Code = 37000
                    }
                };

                _context.Products.AddRange(products);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Initial product data seeded successfully.");
            }
            else
            {
                _logger.LogInformation("Products already exist. Skipping seeding.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during database initialization.");
        }
    }

}