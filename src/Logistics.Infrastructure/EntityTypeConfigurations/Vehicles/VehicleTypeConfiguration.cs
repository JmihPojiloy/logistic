using Logistics.Infrastructure.DatabaseEntity.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Vehicles;

/// <summary>
/// Настройка полей и связей сущности транспорт в БД
/// </summary>
public class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleEntity>
{
    public void Configure(EntityTypeBuilder<VehicleEntity> builder)
    {
        builder.HasKey(vehicle => vehicle.Id);
        
        builder.Property(vehicle => vehicle.Name).HasMaxLength(100).IsRequired();
        builder.Property(vehicle => vehicle.LoadCapacity);
        builder.Property(vehicle => vehicle.MileAge);
        builder.Property(vehicle => vehicle.Status).HasConversion<int>();
    }
}