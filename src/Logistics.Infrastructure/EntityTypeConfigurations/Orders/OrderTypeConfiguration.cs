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
        builder.OwnsOne(order => order.DeliveryCost, costBuilder =>
        {
            costBuilder.Property(p => p.Sum)
                .HasColumnName("CostAmount")
                .HasPrecision(18, 4);

            costBuilder.Property(p => p.Currency)
                .HasColumnName("Currency")
                .HasConversion<int>();
        });
        
        builder.HasOne(order => order.User)
            .WithMany(user => user.Orders)
            .HasForeignKey(order => order.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(order => order.Vehicle)
            .WithMany(vehicle => vehicle.Orders)
            .HasForeignKey(order => order.VehicleId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(order => order.Address)
            .WithMany() 
            .HasForeignKey(order => order.AddressId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(order => order.AddressId);
        builder.HasIndex(order => order.UserId);
        builder.HasIndex(order => order.VehicleId);
    }
}