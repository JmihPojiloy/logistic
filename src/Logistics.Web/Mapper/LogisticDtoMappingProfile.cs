using AutoMapper;
using Logistics.Domain.Entities.Addresses;
using Logistics.Domain.Entities.Delivery;
using Logistics.Domain.Entities.Notifications;
using Logistics.Domain.Entities.Orders;
using Logistics.Domain.Entities.Payments;
using Logistics.Domain.Entities.Products;
using Logistics.Domain.Entities.Promotions;
using Logistics.Domain.Entities.Users;
using Logistics.Domain.Entities.Vehicles;
using Logistics.Domain.Entities.Warehouses;
using Logistics.Web.Dtos.Addresses;
using Logistics.Web.Dtos.Delivery;
using Logistics.Web.Dtos.Notifications;
using Logistics.Web.Dtos.Orders;
using Logistics.Web.Dtos.Payments;
using Logistics.Web.Dtos.Products;
using Logistics.Web.Dtos.Promotions;
using Logistics.Web.Dtos.Users;
using Logistics.Web.Dtos.Vehicles;
using Logistics.Web.Dtos.Warehouses;
using Route = Logistics.Domain.Entities.Delivery.Route;

namespace Logistics.Web.Mapper;

/// <summary>
/// Класс настройки маппинга сущности товар
/// </summary>
public class LogisticDtoMappingProfile : Profile
{
    public LogisticDtoMappingProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();

        CreateMap<DeliveryScheduleDto, DeliverySchedule>().ReverseMap();
        CreateMap<DeliveryTrackingDto, DeliveryTracking>().ReverseMap();
        CreateMap<RouteDto, Route>().ReverseMap();
        
        CreateMap<NotificationDto, Notification>().ReverseMap();
        CreateMap<LetterDto, Letter>().ReverseMap();
        
        CreateMap<OrderDto, Order>().ReverseMap();
        CreateMap<OrderProductDto, OrderProduct>().ReverseMap();
        CreateMap<OrderPromotionDto, OrderPromotion>().ReverseMap();

        CreateMap<PaymentDto, Payment>().ReverseMap();
        CreateMap<RefundedPaymentDto, RefundedPayment>().ReverseMap();
        
        CreateMap<Product, ProductDto>().ReverseMap();
        
        CreateMap<PromotionDto, Promotion>().ReverseMap();
        
        CreateMap<UserDto, User>().ReverseMap();
        
        CreateMap<DriverDto, Driver>().ReverseMap();
        CreateMap<VehicleDto, Vehicle>().ReverseMap();
        CreateMap<VehicleMaintenanceDto, VehicleMaintenance>().ReverseMap();
        
        CreateMap<Warehouse, WarehouseDto>().ReverseMap();
        CreateMap<Inventory, InventoryDto>().ReverseMap();
    }
}