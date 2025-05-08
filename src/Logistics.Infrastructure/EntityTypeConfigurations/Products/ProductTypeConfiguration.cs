using Logistics.Infrastructure.DatabaseEntity.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastructure.EntityTypeConfigurations.Products;

/// <summary>
/// Настройка полей и связей сущности товар для БД
/// </summary>
public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(product => product.Id);
        
        builder.Property(product => product.Name).IsRequired();
        builder.Property(product => product.Description).HasMaxLength(250);
        builder.Property(product => product.Code).IsRequired();
        builder.OwnsOne(p => p.Price, priceBuilder =>
        {
            priceBuilder.Property(p => p.Sum)
                .HasColumnName("PriceAmount")
                .HasPrecision(18, 4);

            priceBuilder.Property(p => p.Currency)
                .HasColumnName("Currency")
                .HasConversion<int>();
        });

        builder.Property(product => product.Weight);
        builder.Property(product => product.Height);
        builder.Property(product => product.Width);
    }
}