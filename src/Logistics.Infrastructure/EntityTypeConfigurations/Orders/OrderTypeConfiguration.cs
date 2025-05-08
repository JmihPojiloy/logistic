using Logistics.Infrastructure.DatabaseEntity.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Orders;

/// <summary>
/// Настройка полей и связей для сущности заказ
/// </summary>
public class OrderTypeConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(order => order.Id);
        
        builder.Property(order => order.Status).HasConversion<int>();
        
        builder.HasOne(order => order.User)
            .WithMany(user => user.Orders)
            .HasForeignKey(order => order.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(order => order.Vehicle)
            .WithMany(vehicle => vehicle.Orders)
            .HasForeignKey(order => order.VehicleId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(order => order.Address)
            .WithOne()
            .HasForeignKey<OrderEntity>(order => order.AddressId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(order => order.AddressId).IsUnique();
        builder.HasIndex(order => order.UserId).IsUnique();
        builder.HasIndex(order => order.VehicleId).IsUnique();
    }
}