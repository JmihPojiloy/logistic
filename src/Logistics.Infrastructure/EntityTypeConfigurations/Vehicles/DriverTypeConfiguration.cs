using Logistics.Infrastructure.DatabaseEntity.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Vehicles;

/// <summary>
/// Настройки полей и связей сущности водитель для БД
/// </summary>
public class DriverTypeConfiguration : IEntityTypeConfiguration<DriverEntity>
{
    public void Configure(EntityTypeBuilder<DriverEntity> builder)
    {
        builder.HasKey(driver => driver.Id);

        builder.Property(driver => driver.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(driver => driver.LastName).HasMaxLength(100).IsRequired();
        builder.Property(driver => driver.MiddleName).HasMaxLength(100);
        builder.Property(driver => driver.Email).HasMaxLength(100);
        builder.Property(driver => driver.PhoneNumber);
        builder.Property(driver => driver.Gender).HasConversion<int>();
        builder.Property(driver => driver.DriverLicense).HasMaxLength(50);
        builder.Property(driver => driver.Status).HasConversion<int>();
        
        builder.HasOne(driver => driver.Vehicle)
            .WithOne(vehicle => vehicle.Driver)
            .HasForeignKey<DriverEntity>(driver => driver.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        builder.HasIndex(driver => driver.VehicleId).IsUnique();
    }
}