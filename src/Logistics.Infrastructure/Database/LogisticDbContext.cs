using Logistics.Domain.ValueObjects;
using Logistics.Infrastructure.DatabaseEntity.Addresses;
using Logistics.Infrastructure.DatabaseEntity.Delivery;
using Logistics.Infrastructure.DatabaseEntity.Notifications;
using Logistics.Infrastructure.DatabaseEntity.Orders;
using Logistics.Infrastructure.DatabaseEntity.Payments;
using Logistics.Infrastructure.DatabaseEntity.Products;
using Logistics.Infrastructure.DatabaseEntity.Promotions;
using Logistics.Infrastructure.DatabaseEntity.Users;
using Logistics.Infrastructure.DatabaseEntity.Vehicles;
using Logistics.Infrastructure.DatabaseEntity.Warehouses;
using Logistics.Infrastructure.EntityTypeConfigurations.Addresses;
using Logistics.Infrastructure.EntityTypeConfigurations.Delivery;
using Logistics.Infrastructure.EntityTypeConfigurations.Notifications;
using Logistics.Infrastructure.EntityTypeConfigurations.Orders;
using Logistics.Infrastructure.EntityTypeConfigurations.Payments;
using Logistics.Infrastructure.EntityTypeConfigurations.Products;
using Logistics.Infrastructure.EntityTypeConfigurations.Promotions;
using Logistics.Infrastructure.EntityTypeConfigurations.Users;
using Logistics.Infrastructure.EntityTypeConfigurations.Vehicles;
using Logistics.Infrastructure.EntityTypeConfigurations.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure.Database;

public class LogisticDbContext : DbContext
{
    public DbSet<AddressEntity> Addresses { get; set; }
    
    //delivery
    public DbSet<DeliveryScheduleEntity> DeliverySchedules { get; set; }
    public DbSet<DeliveryTrackingEntity> DeliveryTrackings { get; set; }
    public DbSet<RouteEntity> Routes { get; set; }
    
    // notifications
    public DbSet<NotificationEntity> Notifications { get; set; }
    public DbSet<LetterEntity> Letters { get; set; }
    
    // orders
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderProductEntity> OrderProducts { get; set; }
    public DbSet<OrderPromotionEntity> OrderPromotions { get; set; }
    
    //payments
    public DbSet<PaymentEntity> Payments { get; set; }
    public DbSet<RefundedPaymentEntity> RefundedPayments { get; set; }
    
    public DbSet<ProductEntity> Products { get; set; }
    
    public DbSet<PromotionEntity> Promotions { get; set; }
    
    public DbSet<UserEntity> Users { get; set; }
    
    //vehicle
    public DbSet<VehicleEntity> Vehicles { get; set; }
    public DbSet<DriverEntity> Drivers { get; set; }
    public DbSet<VehicleMaintenanceEntity> VehicleMaintenances { get; set; }
    
    //warehouse
    public DbSet<WarehouseEntity> Warehouses { get; set; }
    public DbSet<InventoryEntity> Inventories { get; set; }
    
    public LogisticDbContext(DbContextOptions<LogisticDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Owned<Money>();
        
        modelBuilder.ApplyConfiguration(new AddressTypeConfiguration());
        
        modelBuilder.ApplyConfiguration(new DeliveryScheduleTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DeliveryTrackingTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RouteTypeConfiguration());

        modelBuilder.ApplyConfiguration(new NotificationTypeConfiguration());
        modelBuilder.ApplyConfiguration(new LetterTypeConfiguration());

        modelBuilder.ApplyConfiguration(new OrderTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderProductTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderPromotionTypeConfiguration());

        modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RefundedPaymentTypeConfiguration());
        
        modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());

        modelBuilder.ApplyConfiguration(new PromotionTypeConfiguration());
        
        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());

        modelBuilder.ApplyConfiguration(new DriverTypeConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleTypeConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleMaintenanceTypeConfiguration());
        
        modelBuilder.ApplyConfiguration(new WarehouseTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryTypeConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}