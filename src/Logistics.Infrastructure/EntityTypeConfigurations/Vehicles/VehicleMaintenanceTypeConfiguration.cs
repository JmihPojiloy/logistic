using Logistics.Domain.Entities.Vehicles;
using Logistics.Infrastructure.DatabaseEntity.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Vehicles;

/// <summary>
/// Настройки полей и связей сущности техническое обслуживание для БД
/// </summary>
public class VehicleMaintenanceTypeConfiguration : IEntityTypeConfiguration<VehicleMaintenanceEntity>
{
    public void Configure(EntityTypeBuilder<VehicleMaintenanceEntity> builder)
    {
        builder.HasKey(maintenance => maintenance.Id);
        
        builder.Property(maintenance => maintenance.MaintenanceDate);
        builder.Property(maintenance => maintenance.Description);
        builder.OwnsOne(maintenance => maintenance.MaintenancePrice, priceBuilder =>
        {
            priceBuilder.Property(m => m.Sum)
                .HasColumnName("MaintenancePrice")
                .HasPrecision(18, 4);

            priceBuilder.Property(m => m.Currency)
                .HasColumnName("Currency")
                .HasConversion<int>();
        });
        builder.HasOne(maintenance => maintenance.Vehicle)
            .WithMany(vehicle => vehicle.VehicleMaintenance)
            .HasForeignKey(maintenance => maintenance.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(maintenance => maintenance.VehicleId).IsUnique();
    }
}