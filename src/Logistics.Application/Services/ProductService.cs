using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.Services;
using Logistics.Domain.Entities.Products;

namespace Logistics.Application.Services;

/// <summary>
/// Сервисный класс для работы с товаром
/// </summary>
public class ProductService : IService<Product>
{
    private readonly IRepository<Product> _productRepository;
    
    public ProductService(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    
    /// <summary>
    /// Получить все записи товаров
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Неизменяемая коллекция записей товаров</returns>
    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        
        return products;
    }

    /// <summary>
    /// Получить товар по Id
    /// </summary>
    /// <param name="productId">Id товара</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность товара</returns>
    public async Task<Product> GetByIdAsync(int productId, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
        
        return product;
    }

    /// <summary>
    /// Добавить или обновить товар
    /// </summary>
    /// <param name="product">Товар</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Измененный или добавленный товар</returns>
    public async Task<Product> AddOrUpdateAsync(Product product, CancellationToken cancellationToken)
    {
        var processedProduct = await _productRepository.AddOrUpdateAsync(product, cancellationToken);
        
        return processedProduct;
    }

    /// <summary>
    /// Удалить товар по Id
    /// </summary>
    /// <param name="productId">Id товара</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Id удаленного товара либо ошибка NotFound</returns>
    public async Task<int> DeleteAsync(int productId, CancellationToken cancellationToken)
    {
        var result = await _productRepository.DeleteAsync(productId, cancellationToken);
        
        return result;
    }
}