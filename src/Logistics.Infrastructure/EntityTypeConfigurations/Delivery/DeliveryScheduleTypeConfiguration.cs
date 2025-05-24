using Logistics.Infrastructure.DatabaseEntity.Delivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Delivery;

/// <summary>
/// Настройка полей и связей для сущности график доставки в БД
/// </summary>
public class DeliveryScheduleTypeConfiguration : IEntityTypeConfiguration<DeliveryScheduleEntity>
{
    public void Configure(EntityTypeBuilder<DeliveryScheduleEntity> builder)
    {
        builder.HasKey(deliverySchedule => deliverySchedule.Id);

        builder.Property(deliverySchedule => deliverySchedule.EstimatedLoadingDate);
        builder.Property(deliverySchedule => deliverySchedule.EstimatedDeliveryDate);
        builder.Property(deliverySchedule => deliverySchedule.ActualLoadingDate);
        builder.Property(deliverySchedule => deliverySchedule.ActualDeliveryDate);
        
        builder.HasOne(deliverySchedule => deliverySchedule.Order)
            .WithOne()
            .HasForeignKey<DeliveryScheduleEntity>(deliverySchedule => deliverySchedule.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(deliverySchedule => deliverySchedule.OrderId);
    }
}