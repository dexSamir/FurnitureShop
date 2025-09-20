using AutoMapper;
using FurnitureShop.BL.Dtos.ProductDtos;
using FurnitureShop.Core.Entities;

namespace FurnitureShop.BL.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductCreateDto, Product>()
            .ForMember(x=> x.CreatedTime, y=> y.MapFrom(x=> DateTime.UtcNow));

        CreateMap<Product, ProductGetDto>();
        CreateMap<ProductUpdateDto, Product>()
            .ForMember(x=> x.UpdatedTime, y=> y.MapFrom(x=> DateTime.UtcNow))
            .ForMember(x=> x.IsUpdated, y=> y.MapFrom(x=> true))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Product, ProductDetailDto>(); 
    }
}