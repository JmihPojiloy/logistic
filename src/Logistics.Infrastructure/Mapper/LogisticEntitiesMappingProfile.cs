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
using Logistics.Infrastructure.DatabaseEntity;
using Logistics.Infrastructure.DatabaseEntity.Addresses;
using Logistics.Infrastructure.DatabaseEntity.Delivery;
using Logistics.Infrastructure.DatabaseEntity.Notifications;
using Logistics.Infrastructure.DatabaseEntity.Orders;
using Logistics.Infrastructure.DatabaseEntity.Payments;
using Logistics.Infrastructure.DatabaseEntity.Products;
using Logistics.Infrastructure.DatabaseEntity.Promotions;
using Logistics.Infrastructure.DatabaseEntity.Users;
using Logistics.Infrastructure.DatabaseEntity.Vehicles;
using Logistics.Infrastructure.DatabaseEntity.Warehouses;

namespace Logistics.Infrastructure.Mapper;

/// <summary>
/// Настройка маппинга сущностей для БД
/// </summary>
public class LogisticEntitiesMappingProfile : Profile
{
    public LogisticEntitiesMappingProfile()
    {
        CreateMap<AddressEntity, Address>().ReverseMap();
        
        CreateMap<DeliveryScheduleEntity, DeliverySchedule>().ReverseMap();
        CreateMap<DeliveryTrackingEntity, DeliveryTracking>().ReverseMap();
        CreateMap<RouteEntity, Route>().ReverseMap();
        
        CreateMap<NotificationEntity, Notification>().ReverseMap();
        CreateMap<LetterEntity, Letter>().ReverseMap();
        
        CreateMap<OrderEntity, Order>().ReverseMap();
        CreateMap<OrderProductEntity, OrderProduct>().ReverseMap();
        CreateMap<OrderProductEntity, OrderPromotion>().ReverseMap();
        
        CreateMap<PaymentEntity, Payment>().ReverseMap();
        CreateMap<RefundedPaymentEntity, RefundedPayment>().ReverseMap();
        
        CreateMap<ProductEntity, Product>().ReverseMap();
        
        CreateMap<PromotionEntity, Promotion>().ReverseMap();
        
        CreateMap<UserEntity, User>().ReverseMap();
        CreateMap<UserCredentialEntity, UserCredential>().ReverseMap();
        
        CreateMap<DriverEntity, Driver>().ReverseMap();
        CreateMap<VehicleEntity, Vehicle>().ReverseMap();
        CreateMap<VehicleMaintenanceEntity, VehicleMaintenance>().ReverseMap();
        
        CreateMap<WarehouseEntity, Warehouse>().ReverseMap();
        CreateMap<InventoryEntity, Inventory>().ReverseMap();
    }
}