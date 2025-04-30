using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.Services;
using Logistics.Domain.Entities.Products;

namespace Logistics.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        
        return products;
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        
        return product;
    }

    public async Task<Product> AddOrUpdateProductAsync(Product product)
    {
        var processedProduct = await _productRepository.AddOrUpdateProductAsync(product);
        
        return processedProduct;
    }

    public async Task<int> DeleteProductAsync(int productId)
    {
        var result = await _productRepository.DeleteProductAsync(productId);
        
        return result;
    }
}