using Logistics.Infrastructure.DatabaseEntity.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Orders;

/// <summary>
/// Настройки полей и связей для сущности заказ-промоакция в БД
/// </summary>
public class OrderPromotionTypeConfiguration : IEntityTypeConfiguration<OrderPromotionEntity>
{
    public void Configure(EntityTypeBuilder<OrderPromotionEntity> builder)
    {
        builder.HasKey(orderPromotion => orderPromotion.Id);
        
        builder.HasOne(orderPromotion => orderPromotion.Order)
            .WithMany(order => order.OrderPromotions)
            .HasForeignKey(orderPromotion => orderPromotion.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(orderPromotion => orderPromotion.Promotion)
            .WithMany(promotion => promotion.OrderPromotions)
            .HasForeignKey(orderPromotion => orderPromotion.PromotionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(orderPromotion => new { orderPromotion.OrderId, orderPromotion.PromotionId }).IsUnique();
    }
}