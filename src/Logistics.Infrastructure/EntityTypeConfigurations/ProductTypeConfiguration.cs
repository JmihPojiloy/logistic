using Logistics.Domain.Enums;
using Logistics.Domain.ValueObjects;
using Logistics.Infrastructure.DatabaseEntity.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations;

/// <summary>
/// Настройка полей и связей сущности товар для базы данных
/// </summary>
public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(product => product.Id);
        builder.Property(product => product.Id);
        builder.HasIndex(product => product.Id).IsUnique();
        builder.Property(product => product.Name).IsRequired();
        builder.Property(product => product.Description).HasMaxLength(250);
        builder.Property(product => product.Code).IsRequired();
        builder.OwnsOne(product => product.Price, priceBuilder =>
        {
            priceBuilder.Property(p => p.Sum).HasColumnName("PriceAmount");
            priceBuilder.Property(p => p.Currency).HasColumnName("Currency");
        });
    }
}