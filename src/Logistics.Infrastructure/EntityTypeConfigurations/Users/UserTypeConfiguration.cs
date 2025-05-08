using Logistics.Infrastructure.DatabaseEntity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Users;

/// <summary>
/// Класс для настройки полей и связей сущности пользователь в БД
/// </summary>
public class UserTypeConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(user => user.Id);
        
        builder.Property(user => user.FirstName).HasMaxLength(250);
        builder.Property(user => user.LastName).HasMaxLength(250);
        builder.Property(user => user.MiddleName).HasMaxLength(250);
        builder.Property(user => user.Email).HasMaxLength(250);
        builder.Property(user => user.PhoneNumber).IsRequired();
        builder.Property(user => user.Gender).HasConversion<int>();
        
        builder.HasMany(user => user.Addresses)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}