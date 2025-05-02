using Logistics.Domain.Entities.Products;

namespace Logistics.Application.Interfaces.Services;

/// <summary>
/// Интерфейс сервиса для работы с товаром
/// </summary>
public interface IProductService
{
    public Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);
    public Task<Product> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default);
    public Task<Product> AddOrUpdateProductAsync(Product product, CancellationToken cancellationToken = default);
    public Task<int> DeleteProductAsync(int productId, CancellationToken cancellationToken = default);
}