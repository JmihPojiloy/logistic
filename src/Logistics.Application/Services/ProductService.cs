using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.Services;
using Logistics.Domain.Entities.Products;

namespace Logistics.Application.Services;

/// <summary>
/// Сервисный класс для работы с товаром
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    /// <summary>
    /// Получить все записи товаров
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Неизменяемая коллекция записей товаров</returns>
    public async Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductsAsync(cancellationToken);
        
        return products;
    }

    /// <summary>
    /// Получить товар по Id
    /// </summary>
    /// <param name="productId">Id товара</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность товара</returns>
    public async Task<Product> GetProductByIdAsync(int productId, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);
        
        return product;
    }

    /// <summary>
    /// Добавить или обновить товар
    /// </summary>
    /// <param name="product">Товар</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Измененный или добавленный товар</returns>
    public async Task<Product> AddOrUpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        var processedProduct = await _productRepository.AddOrUpdateProductAsync(product, cancellationToken);
        
        return processedProduct;
    }

    /// <summary>
    /// Удалить товар по Id
    /// </summary>
    /// <param name="productId">Id товара</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Id удаленного товара либо ошибка NotFound</returns>
    public async Task<int> DeleteProductAsync(int productId, CancellationToken cancellationToken)
    {
        var result = await _productRepository.DeleteProductAsync(productId, cancellationToken);
        
        return result;
    }
}