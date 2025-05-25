using Logistics.Infrastructure.DatabaseEntity.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Warehouses;

/// <summary>
/// Настройка полей и связей сущности склад для БД
/// </summary>
public class WarehouseTypeConfiguration : IEntityTypeConfiguration<WarehouseEntity>
{
    public void Configure(EntityTypeBuilder<WarehouseEntity> builder)
    {
        builder.HasKey(warehouse => warehouse.Id);

        builder.Property(warehouse => warehouse.Name).HasMaxLength(250);
        builder.Property(warehouse => warehouse.Square);
        builder.Property(warehouse => warehouse.Status).HasConversion<int>();
        
        builder.HasOne(warehouse => warehouse.Address)
            .WithMany()
            .HasForeignKey(warehouse => warehouse.AddressId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        ;
        
        builder.HasIndex(warehouse => warehouse.AddressId);
    }
}