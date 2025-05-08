using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Products;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories;

/// <summary>
/// Класс репозитория доступа к БД для товаров
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly LogisticDbContext _logisticDbContext;
    private readonly IMapper _mapper;

    public ProductRepository(LogisticDbContext logisticDbContext, IMapper mapper)
    {
        _logisticDbContext = logisticDbContext;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод делает выборку всех записей товаров из БД
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Неизменяемая коллекция записей товаров</returns>
    public async Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        var productsEntities = await _logisticDbContext.Products
            .AsNoTracking()
            .Include(p => p.Inventories)
                .ThenInclude(i => i.Warehouse)
                .ToListAsync(cancellationToken: cancellationToken);
        
        var products = _mapper.Map<IReadOnlyList<Product>>(productsEntities);
        
        return products;
    }

    /// <summary>
    /// Метод для получения определенного товара по Id
    /// </summary>
    /// <param name="productId">Id товара</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность товара</returns>
    public async Task<Product> GetProductByIdAsync(int productId, CancellationToken cancellationToken)
    {
        var productEntity = await _logisticDbContext.Products
            .AsNoTracking()
            .Include(p => p.Inventories)
            .ThenInclude(i => i.Warehouse)
            .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken: cancellationToken);
        if (productEntity == null) throw new NotFoundException("Product", productId);
        var product = _mapper.Map<Product>(productEntity);
        
        return product;
    }

    /// <summary>
    /// Метод для добавления или обновления записи о товаре в БД 
    /// </summary>
    /// <param name="product">Товар</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленный или добавленный товар</returns>
    public async Task<Product> AddOrUpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);
        EntityEntry<ProductEntity> result;
        
        if (productEntity.Id == 0)
        {
            productEntity.CreatedOn = DateTime.Now;
            result = await _logisticDbContext.Products
                .AddAsync(productEntity, cancellationToken);
        }
        else
        {
            result = _logisticDbContext.Products.Update(productEntity);
        }

        await _logisticDbContext.SaveChangesAsync(cancellationToken);
        
        var processedProduct = _mapper.Map<Product>(result.Entity);
        
        return processedProduct;
    }

    /// <summary>
    /// Метод удаления записи товара из БД
    /// </summary>
    /// <param name="productId">Id товара</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Id удаленного товара в случае успеха, либо ошибку NotFound</returns>
    public async Task<int> DeleteProductAsync(int productId, CancellationToken cancellationToken)
    {
        var deletedProduct = await _logisticDbContext.Products
            .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken: cancellationToken);
        
        if (deletedProduct == null) throw new NotFoundException("Product", productId);
        
        _logisticDbContext.Products.Remove(deletedProduct);
        await _logisticDbContext.SaveChangesAsync(cancellationToken);
        
        return deletedProduct.Id;

    }
}