using Logistics.Infrastructure.DatabaseEntity.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Notifications;

/// <summary>
/// Настройка полей и связей для сущности письмо в БД
/// </summary>
public class LetterTypeConfiguration : IEntityTypeConfiguration<LetterEntity>
{
    public void Configure(EntityTypeBuilder<LetterEntity> builder)
    {
        builder.HasKey(letter => letter.Id);

        builder.Property(letter => letter.RecipientEmail).HasMaxLength(100).IsRequired();
        builder.Property(letter => letter.Subject).HasMaxLength(100);
        builder.Property(letter => letter.Title).HasMaxLength(100);
        builder.Property(letter => letter.Text).HasMaxLength(250);
        builder.Property(letter => letter.Status).HasConversion<int>();
        builder.Property(letter => letter.Url).HasMaxLength(250);
        builder.Property(letter => letter.SendDate);
        builder.Property(letter => letter.HashCode);
        
        builder.HasOne(letter => letter.Notification)
            .WithOne(notification => notification.Letter)
            .HasForeignKey<LetterEntity>(letter => letter.NotificationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(letter => letter.NotificationId).IsUnique();
    }
}