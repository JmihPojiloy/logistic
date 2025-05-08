using Logistics.Infrastructure.DatabaseEntity.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Notifications;

/// <summary>
/// Настройка полей и связей для сущности уведомления
/// </summary>
public class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationEntity>
{
    public void Configure(EntityTypeBuilder<NotificationEntity> builder)
    {
        builder.HasKey(notification => notification.Id);
        
        builder.Property(notification => notification.Type).HasConversion<int>();
        builder.Property(notification => notification.Title).HasMaxLength(100);
        builder.Property(notification => notification.Text).HasMaxLength(250);
        builder.Property(notification => notification.Status).HasConversion<int>();
        builder.Property(notification => notification.IsEmail)
            .IsRequired()
            .HasDefaultValue(false);
        builder.Property(notification => notification.SendDate);
        builder.Property(notification => notification.HashCode);
        
        builder.HasOne(notification => notification.Recipient)
            .WithMany(recipient => recipient.Notifications)
            .HasForeignKey(notification => notification.RecipientId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(notification => notification.RecipientId).IsUnique();
    }
}