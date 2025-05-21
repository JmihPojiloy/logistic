using Logistics.Infrastructure.DatabaseEntity.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Payments;

/// <summary>
/// Настройки сущности возврат платежа в БД
/// </summary>
public class RefundedPaymentTypeConfiguration : IEntityTypeConfiguration<RefundedPaymentEntity>
{
    public void Configure(EntityTypeBuilder<RefundedPaymentEntity> builder)
    {
        builder.HasKey(refundedPayment => refundedPayment.Id);

        builder.Property(refundedPayment => refundedPayment.ExternalReceiptId);
        builder.Property(refundedPayment => refundedPayment.CancellationDate);
        builder.OwnsOne(refundedPayment => refundedPayment.Amount, amountBuilder =>
        {
            amountBuilder
                .Property(sum => sum.Sum)
                .HasColumnName("AmountSum")
                .HasPrecision(18, 4);
            
            amountBuilder
                .Property(currency => currency.Currency)
                .HasColumnName("Currency")
                .HasConversion<int>();
        });
        
        builder.HasOne(refundedPayment => refundedPayment.Payment)
            .WithMany(payment => payment.CancelledPayments)
            .HasForeignKey(refundedPayment => refundedPayment.PaymentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(refundedPayment => refundedPayment.PaymentId);
    }
}