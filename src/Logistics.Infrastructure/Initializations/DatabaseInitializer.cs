using Logistics.Application.Interfaces.Initializations;
using Logistics.Domain.Enums;
using Logistics.Domain.ValueObjects;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Addresses;
using Logistics.Infrastructure.DatabaseEntity.Products;
using Logistics.Infrastructure.DatabaseEntity.Vehicles;
using Logistics.Infrastructure.DatabaseEntity.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Logistics.Infrastructure.Initializations;

/// <summary>
/// Класс для инициализации БД (для презентации)
/// </summary>
public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly LogisticDbContext _context;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(LogisticDbContext context, ILogger<DatabaseInitializer> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Общий метод инициализации БД
    /// </summary>
    public void Initialize()
    {
        _logger.LogInformation("Starting migration process...");
        try
        {
            _context.Database.Migrate();
            _logger.LogInformation("Database migration completed successfully.");

            if (!_context.Products.Any() && !_context.Warehouses.Any())
            {
                InitProductsAndWarehouse();
            }

            if (!_context.Vehicles.Any())
            {
                InitVehicles();
            }

            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during database initialization.");
        }
    }

    /// <summary>
    /// Инициализация склада с товарами
    /// </summary>
    private void InitProductsAndWarehouse()
    {
        _logger.LogInformation("Seeding initial data for products and warehouse...");

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
                Code = 760000,
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Name = "Смартфон Apple iPhone 14 128GB MPU93CH/A Black",
                Description = "Новый, без дефектов, отличное состояние",
                Price = new Money(54990m, Currency.RUB),
                Weight = 0.17,
                Height = 15,
                Width = 7,
                Code = 37000,
                CreatedOn = DateTime.UtcNow
            }
        };
        
        var warehouse = new WarehouseEntity
        {
            Name = "Тестовый склад",
            Address = new AddressEntity
            {
                Zip = "12345",
                County = "Тест страна",
                City = "Тест город",
                Street = "Тест улица",
                HouseNumber = "1",
                ApartmentNumber = "2",
                Latitude = 28.34,
                Longitude = 33.33,
                CreatedOn = DateTime.UtcNow
            },
            Square = 200,
            Status = WarehouseStatus.Open,
            CreatedOn = DateTime.UtcNow
        };
        
        var inventories = new List<InventoryEntity>
        {
            new()
            {
                Product = products[0],
                Warehouse = warehouse,
                Quantity = 100,
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Product = products[1],
                Warehouse = warehouse,
                Quantity = 50,
                CreatedOn = DateTime.UtcNow
            }
        };

        products[0].Inventories = new List<InventoryEntity> { inventories[0] };
        products[1].Inventories = new List<InventoryEntity> { inventories[1] };
        warehouse.Inventories = inventories;
        
        _context.AddRange(products);
        _context.Warehouses.Add(warehouse);
        _logger.LogInformation("Initial warehouse and product data seeded successfully.");
    }


    /// <summary>
    /// Метод инициализации сущности "Транспорт"
    /// </summary>
    private void InitVehicles()
    {
        _logger.LogInformation("No vehicles found. Seeding initial data...");

        var vehicle = new VehicleEntity
        {
            Name = "Test vehicle",
            LoadCapacity = 30,
            MileAge = 10000,
            Status = VehicleStatus.Free,
            CreatedOn = DateTime.UtcNow
        };

        var driver = new DriverEntity
        {
            FirstName = "Test",
            LastName = "Test",
            MiddleName = "Test",
            Email = "test@test.com",
            DriverLicense = "TT123456",
            Gender = Gender.Male,
            PhoneNumber = 1234567890,
            CreatedOn = DateTime.UtcNow,
            Status = DriverStatus.Free,
            Vehicle = vehicle
        };

        vehicle.Driver = driver;

        var maintenance = new VehicleMaintenanceEntity
        {
            MaintenanceDate = DateTime.UtcNow,
            Description = "Test maintenance",
            MaintenancePrice = new Money(10000m, Currency.RUB),
            CreatedOn = DateTime.UtcNow,
            Vehicle = vehicle
        };

        vehicle.VehicleMaintenance = new List<VehicleMaintenanceEntity> { maintenance };

        _context.Vehicles.Add(vehicle);
        _logger.LogInformation("Initial vehicle data seeded successfully.");
    }
}