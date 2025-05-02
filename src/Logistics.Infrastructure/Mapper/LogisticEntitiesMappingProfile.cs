using AutoMapper;
using Logistics.Domain.Entities.Products;
using Logistics.Infrastructure.DatabaseEntity.Products;

namespace Logistics.Infrastructure.Mapper;

/// <summary>
/// Настройка маппинга сущностей для БД
/// </summary>
public class LogisticEntitiesMappingProfile : Profile
{
    public LogisticEntitiesMappingProfile()
    {
        CreateMap<ProductEntity, Product>().ReverseMap();
    }
}