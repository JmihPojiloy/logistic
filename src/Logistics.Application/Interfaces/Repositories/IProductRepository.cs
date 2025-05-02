using Logistics.Domain.Entities.Products;

namespace Logistics.Application.Interfaces.Repositories;

/// <summary>
/// Интерфейс репозитория товара для доступа к БД
/// </summary>
public interface IProductRepository
{
    public Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);
    public Task<Product> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default);
    public Task<Product> AddOrUpdateProductAsync(Product product, CancellationToken cancellationToken = default);
    public Task<int> DeleteProductAsync(int productId, CancellationToken cancellationToken = default);
}