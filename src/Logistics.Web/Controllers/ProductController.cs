using AutoMapper;
using Logistics.Application.Interfaces.Services;
using Logistics.Domain.Entities.Products;
using Logistics.Domain.Enums;
using Logistics.Web.Dtos.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Web.Controllers;

/// <summary>
/// Контроллер для обработки запросов работы с товаром
/// </summary>
[Route("products")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить все записи товаров
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК со списком товаров</returns>
    [HttpGet("getall")]
    public async Task<IActionResult> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllAsync(cancellationToken);
        var productsDto = _mapper.Map<IReadOnlyList<ProductDto>>(products);
        
        return Ok(productsDto);
    }

    /// <summary>
    /// Получить запись о конкретном товаре по Id
    /// </summary>
    /// <param name="id">Id товара</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>OK с записью товара</returns>
    [HttpGet("getbyid/{id:int}")]
    public async Task<IActionResult> GetProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(id, cancellationToken);
        
        var productDto = _mapper.Map<ProductDto>(product);
        
        return Ok(productDto);
    }

    /// <summary>
    /// Добавить новый товар
    /// </summary>
    /// <param name="productDto">Товар</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>OK с добавленным товаром</returns>
    [HttpPost("add")]
    [Authorize(Roles = nameof(UserRole.Manager))]
    public async Task<IActionResult> AddProductAsync([FromBody]ProductDto productDto, CancellationToken cancellationToken)
    {
        var product = await AddOrUpdateAsync(productDto, cancellationToken);
        
        return Ok(product);
    }

    /// <summary>
    /// Обновить данные о товаре
    /// </summary>
    /// <param name="productDto">Обновляемый товар</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК с обновленным товаром</returns>
    [HttpPut("update")]
    [Authorize(Roles = nameof(UserRole.Manager))]
    public async Task<IActionResult> UpdateProductAsync([FromBody]ProductDto productDto, CancellationToken cancellationToken)
    {
        var product = await AddOrUpdateAsync(productDto, cancellationToken);
        
        return Ok(product);
    }

    /// <summary>
    /// Удалить товар по Id
    /// </summary>
    /// <param name="id">Id товара</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК с Id товара</returns>
    [HttpDelete("delete/{id:int}")]
    [Authorize(Roles = nameof(UserRole.Manager))]
    public async Task<IActionResult> DeleteProductAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _productService.DeleteAsync(id, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Вспомогательный метод для добавления или обновления товара
    /// </summary>
    /// <param name="productDto">Товар</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Товар</returns>
    private async Task<ProductDto> AddOrUpdateAsync(ProductDto productDto, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(productDto);
        var processedProduct = await _productService.AddOrUpdateAsync(product, cancellationToken);
        return _mapper.Map<ProductDto>(processedProduct);
    }
}