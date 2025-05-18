using Logistics.Infrastructure.DatabaseEntity.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Warehouses;

/// <summary>
/// Настройка полей и связей сущности остатков в БД
/// </summary>
public class InventoryTypeConfiguration : IEntityTypeConfiguration<InventoryEntity>
{
    public void Configure(EntityTypeBuilder<InventoryEntity> builder)
    {
        builder.HasKey(inventory => inventory.Id);
        
        builder.Property(inventory => inventory.Quantity);
        
        // shadow property для оптимистической блокировки обновляемого товара
        builder.Property<uint>("Xmin")
            .HasColumnName("xmin")
            .IsRowVersion()
            .ValueGeneratedOnAddOrUpdate();
        
        builder.HasOne(inventory => inventory.Product)
            .WithMany(product => product.Inventories)
            .HasForeignKey(inventory => inventory.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(inventory => inventory.Warehouse)
            .WithMany(warehouse => warehouse.Inventories)
            .HasForeignKey(inventory => inventory.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(inventory => new {inventory.ProductId, inventory.WarehouseId}).IsUnique();
    }
}