using Logistics.Infrastructure.DatabaseEntity.Promotions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Promotions;

/// <summary>
/// Настройка полей и связей сущности промоакция в БД
/// </summary>
public class PromotionTypeConfiguration : IEntityTypeConfiguration<PromotionEntity>
{
    public void Configure(EntityTypeBuilder<PromotionEntity> builder)
    {
        builder.HasKey(promotion => promotion.Id);

        builder.Property(promotion => promotion.Code);
        builder.Property(promotion => promotion.Description);
        builder.Property(promotion => promotion.Discount);
        builder.Property(promotion => promotion.StartDate);
        builder.Property(promotion => promotion.EndDate);
    }
}