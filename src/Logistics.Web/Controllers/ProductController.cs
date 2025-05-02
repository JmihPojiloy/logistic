using AutoMapper;
using Logistics.Application.Interfaces.Services;
using Logistics.Domain.Entities.Products;
using Logistics.Web.Dtos.Products;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Web.Controllers;

/// <summary>
/// Контроллер для обработки запросов работы с товаром
/// </summary>
[Route("products")]
[ApiController]
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
    [Route("getall")]
    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllProductsAsync(cancellationToken);
        var productsDto = _mapper.Map<IReadOnlyList<ProductDto>>(products);
        
        return Ok(productsDto);
    }

    /// <summary>
    /// Получить запись о конкретном товаре по Id
    /// </summary>
    /// <param name="id">Id товара</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>OK с записью товара</returns>
    [Route("getbyid{id:int}")]
    [HttpGet]
    public async Task<IActionResult> GetProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductByIdAsync(id, cancellationToken);
        
        var productDto = _mapper.Map<ProductDto>(product);
        
        return Ok(productDto);
    }

    /// <summary>
    /// Добавить новый товар
    /// </summary>
    /// <param name="productDto">Товар</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>OK с добавленным товаром</returns>
    [Route("add")]
    [HttpPost]
    public async Task<IActionResult> AddProductAsync([FromBody]ProductDto productDto, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(productDto);
        var processedProduct = await _productService.AddOrUpdateProductAsync(product, cancellationToken);
        var processedProductDto = _mapper.Map<ProductDto>(processedProduct);
        
        return Ok(processedProductDto);
    }

    /// <summary>
    /// Обновить данные о товаре
    /// </summary>
    /// <param name="productDto">Обновляемый товар</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК с обновленным товаром</returns>
    [Route("update")]
    [HttpPut]
    public async Task<IActionResult> UpdateProductAsync(ProductDto productDto, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(productDto);
        var processedProduct = await _productService.AddOrUpdateProductAsync(product, cancellationToken);
        var processedProductDto = _mapper.Map<ProductDto>(processedProduct);
        
        return Ok(processedProductDto);
    }

    /// <summary>
    /// Удалить товар по Id
    /// </summary>
    /// <param name="id">Id товара</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК с Id товара</returns>
    [Route("delete{id:int}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteProductAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _productService.DeleteProductAsync(id, cancellationToken);
        
        return Ok(result);
    }
}