using Logistics.Domain.Entities.Products;

namespace Logistics.Application.Interfaces.Repositories;

public interface IProductRepository
{
    public Task<IReadOnlyList<Product>> GetAllProductsAsync();
    public Task<Product> GetProductByIdAsync(int productId);
    public Task<Product> AddOrUpdateProductAsync(Product product);
    public Task<int> DeleteProductAsync(int productId);
}