using AutoMapper;
using Logistics.Domain.Entities.Products;
using Logistics.Web.Dtos.Products;

namespace Logistics.Web.Mapper;

/// <summary>
/// Класс настройки маппинга сущности товар
/// </summary>
public class LogisticDtoMappingProfile : Profile
{
    public LogisticDtoMappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}