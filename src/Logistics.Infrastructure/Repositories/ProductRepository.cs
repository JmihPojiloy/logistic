using AutoMapper;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Products;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Products;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly LogisticDbContext _logisticDbContext;
    private readonly IMapper _mapper;

    public ProductRepository(LogisticDbContext logisticDbContext, IMapper mapper)
    {
        _logisticDbContext = logisticDbContext;
        _mapper = mapper;
    }
    
    public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
    {
        var productsEntities = await _logisticDbContext.Products.ToListAsync();
        var products = _mapper.Map<IReadOnlyList<Product>>(productsEntities);
        
        return products;
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        var productEntity = await _logisticDbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        var product = _mapper.Map<Product>(productEntity);
        
        return product;
    }

    public async Task<Product> AddOrUpdateProductAsync(Product product)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);

        if (productEntity.Id == 0)
        {
            await _logisticDbContext.Products.AddAsync(productEntity);
        }
        else
        {
            _logisticDbContext.Products.Update(productEntity);
        }

        await _logisticDbContext.SaveChangesAsync();
        
        var processedProduct = _mapper.Map<Product>(productEntity);
        
        return processedProduct;
    }

    public async Task<int> DeleteProductAsync(int productId)
    {
        var deletedProduct = await _logisticDbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (deletedProduct != null)
        {
            _logisticDbContext.Products.Remove(deletedProduct);
            await _logisticDbContext.SaveChangesAsync();
        
            return deletedProduct.Id;
        }
            
        return 0;
    }
}