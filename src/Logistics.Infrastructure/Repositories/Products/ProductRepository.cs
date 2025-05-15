using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Filters;
using Logistics.Application.Interfaces.Filters;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Products;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Products;

/// <summary>
/// Класс репозитория доступа к БД для товаров
/// </summary>
public class ProductRepository : IRepository<Product>
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
    /// <param name="filter">Фильтр записей</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Неизменяемая коллекция записей товаров</returns>
    public async Task<IReadOnlyList<Product>> GetAllByFilterAsync(IFilter? filter, CancellationToken cancellationToken)
    {
        var query = _logisticDbContext.Products.AsQueryable();
        if (filter is ProductFilter productFilter)
        {
            if (productFilter.ProductsIds.Any())
            {
                query = query.Where(x => productFilter.ProductsIds.Contains(x.Id));
            }
        }
        var productsEntities = await query
            .AsNoTracking()
            .Include(p => p.Inventories)
                .ThenInclude(i => i.Warehouse)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return _mapper.Map<IReadOnlyList<Product>>(productsEntities);
    }

    /// <summary>
    /// Метод для получения определенного товара по Id
    /// </summary>
    /// <param name="productId">Id товара</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность товара</returns>
    public async Task<Product> GetByIdAsync(int productId, CancellationToken cancellationToken)
    {
        var productEntity = await _logisticDbContext.Products
            .AsNoTracking()
            .Include(p => p.Inventories)
                .ThenInclude(i => i.Warehouse)
            .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken: cancellationToken);
        if (productEntity == null) throw new NotFoundException("Product", productId);
        
        return _mapper.Map<Product>(productEntity);
    }

    /// <summary>
    /// Метод для добавления или обновления записи о товаре в БД 
    /// </summary>
    /// <param name="product">Товар</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленный или добавленный товар</returns>
    public async Task<Product> AddOrUpdateAsync(Product product, CancellationToken cancellationToken)
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
        
        return _mapper.Map<Product>(result.Entity);
    }

    /// <summary>
    /// Метод удаления записи товара из БД
    /// </summary>
    /// <param name="productId">Id товара</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Id удаленного товара в случае успеха, либо ошибку NotFound</returns>
    public async Task<int> DeleteAsync(int productId, CancellationToken cancellationToken)
    {
        var deletedProduct = await _logisticDbContext.Products
            .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken: cancellationToken);
        if (deletedProduct == null) throw new NotFoundException("Product", productId);
        _logisticDbContext.Products.Remove(deletedProduct);
        
        return deletedProduct.Id;
    }
}