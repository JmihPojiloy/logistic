using AutoMapper;
using Logistics.Application.Interfaces.Services;
using Logistics.Domain.Entities.Products;
using Logistics.Web.Dtos.Products;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Web.Controllers;

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

    [Route("getall")]
    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        var products = await _productService.GetAllProductsAsync();
        var productsDto = _mapper.Map<IReadOnlyList<ProductDto>>(products);
        
        return Ok(productsDto);
    }

    [Route("getbyid{id:int}")]
    [HttpGet]
    public async Task<IActionResult> GetProductByIdAsync(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        
        var productDto = _mapper.Map<ProductDto>(product);
        
        return Ok(productDto);
    }

    [Route("add")]
    [HttpPost]
    public async Task<IActionResult> AddProductAsync([FromBody]ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        var processedProduct = await _productService.AddOrUpdateProductAsync(product);
        var processedProductDto = _mapper.Map<ProductDto>(processedProduct);
        
        return Ok(processedProductDto);
    }

    [Route("update")]
    [HttpPut]
    public async Task<IActionResult> UpdateProductAsync(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        var processedProduct = await _productService.AddOrUpdateProductAsync(product);
        var processedProductDto = _mapper.Map<ProductDto>(processedProduct);
        
        return Ok(processedProductDto);
    }

    [Route("delete{id:int}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        var result = await _productService.DeleteProductAsync(id);
        
        return Ok(result);
    }
}