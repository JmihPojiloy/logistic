using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.Services;
using Logistics.Application.Interfaces.UnitOfWork;
using Logistics.Domain.Entities.Products;

namespace Logistics.Application.Services;

/// <summary>
/// Сервисный класс для работы с товаром
/// </summary>
public class ProductService : IService<Product>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    /// <summary>
    /// Получить все записи товаров
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Неизменяемая коллекция записей товаров</returns>
    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<Product>();
        var products = await repo.GetAllAsync(cancellationToken);
        
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
        var repo = _unitOfWork.GetRepository<Product>();
        var product = await repo.GetByIdAsync(productId, cancellationToken);
        
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
        var repo = _unitOfWork.GetRepository<Product>();
        var processedProduct = await repo.AddOrUpdateAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
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
        var repo = _unitOfWork.GetRepository<Product>();
        var result = await repo.DeleteAsync(productId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}