using AutoMapper;
using Logistics.Domain.Entities.Products;
using Logistics.Infrastructure.DatabaseEntity.Products;

namespace Logistics.Infrastructure.Mapper;

public class LogisticEntitiesMappingProfile : Profile
{
    public LogisticEntitiesMappingProfile()
    {
        CreateMap<ProductEntity, Product>().ReverseMap();
    }
}