using AutoMapper;
using Logistics.Domain.Entities.Products;
using Logistics.Web.Dtos.Products;

namespace Logistics.Web.Mapper;

public class LogisticDtoMappingProfile : Profile
{
    public LogisticDtoMappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}