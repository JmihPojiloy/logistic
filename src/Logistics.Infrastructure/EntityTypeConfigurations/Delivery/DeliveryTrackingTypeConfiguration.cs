using Logistics.Infrastructure.DatabaseEntity.Delivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Delivery;

/// <summary>
/// Настройка полей и связей для сущности отслеживание транспорта в БД
/// </summary>
public class DeliveryTrackingTypeConfiguration : IEntityTypeConfiguration<DeliveryTrackingEntity>
{
    public void Configure(EntityTypeBuilder<DeliveryTrackingEntity> builder)
    {
        builder.HasKey(tracking => tracking.Id);

        builder.Property(tracking => tracking.Latitude);
        builder.Property(tracking => tracking.Longitude);
        
        builder.HasOne(tracking => tracking.Vehicle)
            .WithOne()
            .HasForeignKey<DeliveryTrackingEntity>(tracking => tracking.VehicleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(tracking => tracking.VehicleId);
    }
}