using Logistics.Application.Exceptions;
using Logistics.Application.Filters;
using Logistics.Application.Interfaces.Geo;
using Logistics.Application.Interfaces.Payments;
using Logistics.Application.Interfaces.Services;
using Logistics.Application.Interfaces.UnitOfWork;
using Logistics.Domain.Entities.Addresses;
using Logistics.Domain.Entities.Delivery;
using Logistics.Domain.Entities.Notifications;
using Logistics.Domain.Entities.Orders;
using Logistics.Domain.Entities.Payments;
using Logistics.Domain.Entities.Products;
using Logistics.Domain.Entities.Promotions;
using Logistics.Domain.Entities.Vehicles;
using Logistics.Domain.Entities.Warehouses;
using Logistics.Domain.Enums;
using Logistics.Domain.Extensions;
using Logistics.Domain.ValueObjects;

namespace Logistics.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentService _paymentService;
    private readonly IGeoService _geoService;
    private const decimal CostByKm = 100.00m;
    private const int CarSpeedAverage = 60;

    public OrderService(IUnitOfWork unitOfWork, IPaymentService paymentService, IGeoService geoService)
    {
        _unitOfWork = unitOfWork;
        _paymentService = paymentService;
        _geoService = geoService;
    }

    public async Task<IReadOnlyCollection<Order>> GetAllAsync(CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<Order>();
        return await repo.GetAllByFilterAsync(null, cancellationToken);
    }

    /// <summary>
    /// Метод оформления заказа без оплаты
    /// </summary>
    /// <param name="order">Заказ</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Оформленный заказ</returns>
    /// <exception cref="OrderException">Ошибка оформления заказа</exception>
    public async Task<Order> AddOrUpdateAsync(Order order, CancellationToken cancellationToken)
    {
        var orderRepo = _unitOfWork.GetRepository<Order>();
        var addressRepo = _unitOfWork.GetRepository<Address>();
        var routeRepo = _unitOfWork.GetRepository<Route>();
        var productRepo = _unitOfWork.GetRepository<Product>();

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            var deliveryAddress = order.Address;

            if (deliveryAddress.Latitude == null || deliveryAddress.Longitude == null)
            {
                var fullAddress = order.Address.GetAddressForGeocoding();
                var (latitude, longitude) = await _geoService.GetCoordinates(fullAddress, cancellationToken);
                deliveryAddress.Latitude = latitude;
                deliveryAddress.Longitude = longitude;
                await addressRepo.AddOrUpdateAsync(deliveryAddress, cancellationToken);
            }

            var productId = order.OrderProducts.FirstOrDefault()?.ProductId ?? 0;
            var product = await productRepo.GetByIdAsync(productId, cancellationToken);
            var shippingAddress = product.Inventories.FirstOrDefault()?.Warehouse.Address;

            if (shippingAddress == null) throw new OrderException(order.Id);

            if (shippingAddress.Latitude == null || shippingAddress.Longitude == null)
            {
                var fullAddress = shippingAddress.GetAddressForGeocoding();
                var (latitude, longitude) = await _geoService.GetCoordinates(fullAddress, cancellationToken);
                shippingAddress.Latitude = latitude;
                shippingAddress.Longitude = longitude;
                await addressRepo.AddOrUpdateAsync(shippingAddress, cancellationToken);
            }

            var route = new Route
            {
                AddressId = order.Address.Id,
                Address = order.Address,
                Cost = new Money(CostByKm, product.Price!.Currency),
                CreatedDate = DateTime.UtcNow
            };
            route.SetDistance(shippingAddress, deliveryAddress);

            if (route.Distance != null && route.Cost != null)
            {
                var deliveryCost = route.Distance.Value * CostByKm;
                order.DeliveryCost = new Money(deliveryCost, product.Price!.Currency);
            }
            
            await routeRepo.AddOrUpdateAsync(route, cancellationToken);

            var result = await orderRepo.AddOrUpdateAsync(order, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);
            
            return result;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw new OrderException(order.Id);
        }
    }

    /// <summary>
    /// Удалить заказ
    /// </summary>
    /// <param name="id">ID заказа</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ID удаленного заказа</returns>
    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<Order>();
        var result = await repo.DeleteAsync(id, cancellationToken);
        
        return result;
    }

    /// <summary>
    /// Оплатить заказ
    /// </summary>
    /// <param name="order">Заказ</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Оплаченный заказ</returns>
    /// <exception cref="OrderException">Ошибка оплаты заказа</exception>
    public async Task<Order> PayOrderAsync(Order order, CancellationToken cancellationToken)
    {
        var orderRepo = _unitOfWork.GetRepository<Order>();
        var paymentRepo = _unitOfWork.GetRepository<Payment>();
        var productRepo = _unitOfWork.GetRepository<Product>();
        var inventoryRepo = _unitOfWork.GetRepository<Inventory>();
        var promotionRepo = _unitOfWork.GetRepository<Promotion>();

        var processedOrder = await orderRepo.GetByIdAsync(order.Id, cancellationToken);
        
        var productFilter = new ProductFilter
        {
            ProductsIds = order.OrderProducts.Select(x => x.ProductId).ToList() 
        };
        
        var products = await productRepo.GetAllByFilterAsync(productFilter, cancellationToken);
        
        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            
            foreach (var product in products)
            {
                var orderProduct = order.OrderProducts.FirstOrDefault(x => x.ProductId == product.Id);
                var inventory =  product.Inventories.FirstOrDefault();
                if (inventory != null && orderProduct != null)
                {
                    if (inventory.Quantity == 0 || orderProduct.Quantity > inventory.Quantity)
                    {
                        throw new OrderException(order.Id, "Не достаточно товара на складе.");
                    }
                    inventory.Quantity -= orderProduct.Quantity;
                    await inventoryRepo.AddOrUpdateAsync(inventory, cancellationToken);
                }
            }

            var promotionFilter = new PromotionsFilter
            {
                PromotionsIds = processedOrder.OrderPromotions.Select(p => p.PromotionId).ToList(),
                EndDate = DateTime.UtcNow
            };
            
            var promotions = await promotionRepo.GetAllByFilterAsync(promotionFilter, cancellationToken);
            
            var promotion = promotions.Where(p => p.Discount.HasValue).MaxBy(p => p.Discount!.Value);
            var discount = (promotion != null && promotion.Discount.HasValue) ? promotion.Discount.Value / 100  : 0;
            var sum = discount > 0 ? products.Sum(p => p.Price!.Sum) * discount : products.Sum(p => p.Price!.Sum); 
            var productsAmount = new Money(sum, products.FirstOrDefault()!.Price!.Currency);
            if(processedOrder.DeliveryCost == null) throw new OrderException(processedOrder.Id, "Не указана стоимость доставки");
            var paymentAmount = productsAmount + processedOrder.DeliveryCost;
            
            var payment = new Payment
            {
                OrderId = processedOrder.Id,
                Order = processedOrder,
                CreatedDate = DateTime.UtcNow,
                Amount = paymentAmount
            };
        
            payment.ExternalReceiptId = _paymentService.InitPayment(payment);
            payment.PaymentDate = DateTime.UtcNow;
            await paymentRepo.AddOrUpdateAsync(payment, cancellationToken);
        
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw new OrderException(order.Id, $"Ошибка при оплате заказа {ex.Message}");
        }

        await FillRoute(processedOrder, cancellationToken);
        await CreateTrackingAndSchedule(processedOrder, cancellationToken);
        
        var notificationRepo = _unitOfWork.GetRepository<Notification>();

        var notification = processedOrder.User.Notify()
            .WithTitle("Заказ успешно оформлен!")
            .WithText($"Ваш заказ №{processedOrder.Id} оплачен и уже отправлен в доставку.")
            .WithType(NotificationType.Payment)
            .Build();
        
        await notificationRepo.AddOrUpdateAsync(notification, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return processedOrder;
    }

    /// <summary>
    /// Вспомогательный метод формирования маршрута
    /// </summary>
    /// <param name="processedOrder">Заказ</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <exception cref="OrderException">Ошибка оформления заказа</exception>
    private async Task FillRoute(Order processedOrder, CancellationToken cancellationToken)
    {
        var driverRepo = _unitOfWork.GetRepository<Driver>();
        var routeRepo = _unitOfWork.GetRepository<Route>();
        var vehicleRepo = _unitOfWork.GetRepository<Vehicle>();

        var driverFilter = new DriverFilter
        {
            Status = DriverStatus.Free
        };
        var drivers = await driverRepo.GetAllByFilterAsync(driverFilter, cancellationToken);
        var driver = drivers.FirstOrDefault();

        var vehicleFilter = new VehicleFilter
        {
            Status = VehicleStatus.Free
        };
        var vehicles = await vehicleRepo.GetAllByFilterAsync(vehicleFilter, cancellationToken);
        var vehicle = vehicles.FirstOrDefault();

        var routeFilter = new RouteFilter
        {
            AddressId = processedOrder.AddressId,
        };
        var routes = await routeRepo.GetAllByFilterAsync(routeFilter, cancellationToken);
        var route = routes.FirstOrDefault();
        
        if(route == null || driver == null || vehicle == null) 
            throw new OrderException(processedOrder.Id, "Ошибка в формировании маршрута.");
        
        vehicle.Driver = driver;
        vehicle.Route = route;
        vehicle.Status = VehicleStatus.OnRoute;
        vehicle.Orders.Add(processedOrder);
        
        driver.Vehicle = vehicle;
        driver.VehicleId = vehicle.Id;
        driver.Status = DriverStatus.OnRoute;
        
        route.Vehicle = vehicle;
        route.VehicleId = vehicle.Id;
        
        if (route.Distance.HasValue && CarSpeedAverage > 0)
        {
            double hours = route.Distance.Value! / CarSpeedAverage;
            route.LeadTime = TimeSpan.FromHours(hours);
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await vehicleRepo.AddOrUpdateAsync(vehicle, cancellationToken);
            await driverRepo.AddOrUpdateAsync(driver, cancellationToken);
            await routeRepo.AddOrUpdateAsync(route, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw new OrderException(processedOrder.Id, "Ошибка при оформлении маршрута");
        }
    }

    /// <summary>
    /// Вспомогательный метод формирования отслеживания и графика заказа
    /// </summary>
    /// <param name="processedOrder">Заказ</param>
    /// <param name="cancellationToken">Токен отмены</param>
    private async Task CreateTrackingAndSchedule(Order processedOrder, CancellationToken cancellationToken)
    {
        var deliveryScheduleRepo = _unitOfWork.GetRepository<DeliverySchedule>();
        var deliveryTrackingRepo = _unitOfWork.GetRepository<DeliveryTracking>();
        var routeRepo = _unitOfWork.GetRepository<Route>();

        var routeFilter = new RouteFilter
        {
            VehicleId = processedOrder.VehicleId,
        };
        
        var routes = await routeRepo.GetAllByFilterAsync(routeFilter, cancellationToken);
        var route = routes.FirstOrDefault();
        
        var deliverySchedule = new DeliverySchedule
        {
            OrderId = processedOrder.Id,
            Order = processedOrder,
            CreatedDate = DateTime.UtcNow,
            EstimatedLoadingDate = DateTime.UtcNow,
            EstimatedDeliveryDate = route!.LeadTime.HasValue ? DateTime.UtcNow + route.LeadTime.Value : null,
        };

        var deliveryTracking = new DeliveryTracking
        {
            Vehicle = processedOrder.Vehicle!,
            VehicleId = processedOrder.VehicleId,
            CreatedDate = DateTime.UtcNow,
        };
        
        await deliveryTrackingRepo.AddOrUpdateAsync(deliveryTracking, cancellationToken);
        await deliveryScheduleRepo.AddOrUpdateAsync(deliverySchedule, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}