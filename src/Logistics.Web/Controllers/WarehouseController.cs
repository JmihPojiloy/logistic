using AutoMapper;
using Logistics.Application.Interfaces.Services;
using Logistics.Domain.Entities.Warehouses;
using Logistics.Domain.Enums;
using Logistics.Web.Dtos.Warehouses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Web.Controllers;

/// <summary>
/// Контроллер для обработки запросов работы со складом
/// </summary>
[Route("warehouses")]
[ApiController]
[Authorize]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _service;
    private readonly IMapper _mapper;

    public WarehouseController(IWarehouseService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить все склады
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК со списком складов</returns>
    [HttpGet("getall")]
    [Authorize(Roles = nameof(UserRole.Manager))]
    public async Task<IActionResult> GetAllWarehousesAsync(CancellationToken cancellationToken)
    {
        var warehouses = await _service.GetAllAsync(cancellationToken);
        var warehouseDtos = _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
        
        return Ok(warehouseDtos);
    }

    /// <summary>
    /// Получить склад по Id
    /// </summary>
    /// <param name="id">Id склада</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК с записью склада</returns>
    [HttpGet("getbyid/{id:int}")]
    [Authorize(Roles = nameof(UserRole.Manager))]
    public async Task<IActionResult> GetWarehouseByIdAsync(int id, CancellationToken cancellationToken)
    {
        var warehouse = await _service.GetByIdAsync(id, cancellationToken);
        var warehouseDto = _mapper.Map<WarehouseDto>(warehouse);
        
        return Ok(warehouseDto);
    }

    /// <summary>
    /// Добавить новый склад
    /// </summary>
    /// <param name="warehouseDto">Склад</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК с добавленным складом</returns>
    [HttpPost("add")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> AddWarehouseAsync([FromBody] WarehouseDto warehouseDto,
        CancellationToken cancellationToken)
    {
        var warehouse = await AddOrUpdateAsync(warehouseDto, cancellationToken);
        return Ok(warehouse);
    }

    /// <summary>
    /// Изменит данные о складе
    /// </summary>
    /// <param name="warehouseDto">Склад</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК с измененным складом</returns>
    [HttpPut("update")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> UpdateWarehouseAsync([FromBody] WarehouseDto warehouseDto,
        CancellationToken cancellationToken)
    {
        var warehouse = await AddOrUpdateAsync(warehouseDto, cancellationToken);
        return Ok(warehouse);
    }

    /// <summary>
    /// Удалить склад
    /// </summary>
    /// <param name="id">Id склада</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК с ID удаленного склада</returns>
    [HttpDelete("delete/{id:int}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> DeleteWarehouseAsync(int id, CancellationToken cancellationToken)
    {
        var warehouse = await _service.DeleteAsync(id, cancellationToken);
        
        return Ok(warehouse);
    }

    /// <summary>
    /// Вспомогательный метод для добавления или обновления склада 
    /// </summary>
    /// <param name="warehouseDto">Склад</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Измененный или добавленный склад</returns>
    private async Task<WarehouseDto> AddOrUpdateAsync(WarehouseDto warehouseDto, CancellationToken cancellationToken)
    {
        var warehouse = _mapper.Map<Warehouse>(warehouseDto);
        var processedWarehouse = await _service.AddOrUpdateAsync(warehouse, cancellationToken);
        return _mapper.Map<WarehouseDto>(processedWarehouse);
    }
}