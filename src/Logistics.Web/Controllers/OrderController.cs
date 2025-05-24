using AutoMapper;
using Logistics.Application.Interfaces.Services;
using Logistics.Domain.Entities.Orders;
using Logistics.Domain.Enums;
using Logistics.Web.Dtos.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Web.Controllers;

/// <summary>
/// Контроллер для обработки запросов для работы с заказом
/// </summary>
[Route("orders")]
[ApiController]
[Authorize(Roles = nameof(UserRole.User))]
public class OrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Добавить новый заказ
    /// </summary>
    /// <param name="orderDto">Заказ</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК с добавленным заказом</returns>
    [HttpPost("add")]
    public async Task<IActionResult> AddOrderAsync(OrderDto orderDto, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(orderDto);
        var resultOrder = await _orderService.AddOrUpdateAsync(order, cancellationToken);
        var result = _mapper.Map<OrderDto>(resultOrder);
        
        return Ok(result);
    }

    /// <summary>
    /// Оплатить заказ
    /// </summary>
    /// <param name="orderDto">Заказ</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК с оплаченным заказом</returns>
    [HttpPost("pay")]
    public async Task<IActionResult> PayOrderAsync(OrderDto orderDto, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(orderDto);
        var paidOrder = await _orderService.PayOrderAsync(order, cancellationToken);
        var result = _mapper.Map<PaidOrderDto>(paidOrder);
        
        return Ok(result);
    }
}