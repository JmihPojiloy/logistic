using Logistics.Domain.Entities.Products;

namespace Logistics.Application.Interfaces.Services;

public interface IProductService
{
    public Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<Product> AddOrUpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default);
}