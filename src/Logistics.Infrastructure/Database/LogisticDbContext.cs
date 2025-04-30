using Logistics.Domain.Entities.Products;
using Logistics.Infrastructure.DatabaseEntity.Products;
using Logistics.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure.Database;

public class LogisticDbContext : DbContext
{
    public DbSet<ProductEntity> Products { get; set; }
    
    public LogisticDbContext(DbContextOptions<LogisticDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}