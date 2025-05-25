using Logistics.Domain.Enums;
using Logistics.Domain.ValueObjects;
using Logistics.Infrastructure.DatabaseEntity.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Payments;

/// <summary>
/// Настройки полей и связей сущности платеж в БД
/// </summary>
public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentEntity>
{
    public void Configure(EntityTypeBuilder<PaymentEntity> builder)
    {
        builder.HasKey(payment => payment.Id);

        builder.Property(payment => payment.ExternalReceiptId);
        builder.OwnsOne(p => p.Amount, amountBuilder =>
        {
            amountBuilder.Property(p => p.Sum)
                .HasColumnName("PaymentAmount")
                .HasPrecision(18, 4);

            amountBuilder.Property(p => p.Currency)
                .HasColumnName("Currency")
                .HasConversion<int>();
        });
        builder.Property(payment => payment.PaymentDate);
        
        builder.HasOne(payment => payment.Order)
            .WithMany()
            .HasForeignKey(payment => payment.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(payment => payment.OrderId);
    }
}