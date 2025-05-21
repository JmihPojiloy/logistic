using Logistics.Infrastructure.DatabaseEntity.Delivery;
using Logistics.Infrastructure.DatabaseEntity.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Delivery;

/// <summary>
/// Настройка полей и связей для сущности маршрут в БД
/// </summary>
public class RouteTypeConfiguration : IEntityTypeConfiguration<RouteEntity>
{
    public void Configure(EntityTypeBuilder<RouteEntity> builder)
    {
        builder.HasKey(route => route.Id);

        builder.Property(route => route.Distance);
        builder.Property(route => route.LeadTime);
        builder.OwnsOne(route => route.Cost, costBuilder =>
        {
            costBuilder.Property(p => p.Sum)
                .HasColumnName("CostAmount")
                .HasPrecision(18, 4);

            costBuilder.Property(p => p.Currency)
                .HasColumnName("Currency")
                .HasConversion<int>();
        });
        
        builder.HasOne(route => route.Vehicle)
            .WithOne(vehicle => vehicle.Route)
            .HasForeignKey<RouteEntity>(route => route.VehicleId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(route => route.Address)
            .WithOne()
            .HasForeignKey<RouteEntity>(route => route.AddressId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(route => route.AddressId);
        builder.HasIndex(route => route.VehicleId);
    }
}