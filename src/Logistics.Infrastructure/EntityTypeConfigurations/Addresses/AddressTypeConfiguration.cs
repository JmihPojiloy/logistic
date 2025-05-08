using Logistics.Infrastructure.DatabaseEntity.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Addresses;

/// <summary>
/// Настройка полей и связей для сущности адрес для БД
/// </summary>
public class AddressTypeConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.HasKey(address => address.Id);

        builder.Property(address => address.Zip).HasMaxLength(50);
        builder.Property(address => address.County).HasMaxLength(100);
        builder.Property(address => address.City).HasMaxLength(100);
        builder.Property(address => address.Street).HasMaxLength(250);
        builder.Property(address => address.HouseNumber).HasMaxLength(20);
        builder.Property(address => address.ApartmentNumber).HasMaxLength(20);
        builder.Property(address => address.Latitude);
        builder.Property(address => address.Longitude);
    }
}