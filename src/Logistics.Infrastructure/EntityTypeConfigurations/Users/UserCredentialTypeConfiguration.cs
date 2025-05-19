using Logistics.Infrastructure.DatabaseEntity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Users;

public class UserCredentialTypeConfiguration : IEntityTypeConfiguration<UserCredentialEntity>
{
    public void Configure(EntityTypeBuilder<UserCredentialEntity> builder)
    {
        builder.HasKey(userCredential => userCredential.Id);

        builder.HasIndex(userCredential => userCredential.Phone).IsUnique();
        builder.Property(userCredential => userCredential.PasswordHash);
        builder.Property(userCredential => userCredential.Role).HasConversion<int>();
        
        builder.HasOne(userCredential => userCredential.User)
            .WithOne()
            .HasForeignKey<UserCredentialEntity>(userCredential => userCredential.UserId);
    }
}