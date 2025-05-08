using Logistics.Infrastructure.DatabaseEntity.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Orders;

/// <summary>
/// Поля и связи для сущности заказ-товар для БД
/// </summary>
public class OrderProductTypeConfiguration : IEntityTypeConfiguration<OrderProductEntity>
{
    public void Configure(EntityTypeBuilder<OrderProductEntity> builder)
    {
        builder.HasKey(orderProduct => orderProduct.Id);
        
        builder.Property(orderProduct => orderProduct.Quantity);
        
        builder.HasOne(orderProduct => orderProduct.Order)
            .WithMany(order => order.OrderProducts)
            .HasForeignKey(orderProduct => orderProduct.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(orderProduct => orderProduct.Product)
            .WithMany()
            .HasForeignKey(orderProduct => orderProduct.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(orderProduct => orderProduct.ProductId);
    }
}