using AutoMapper;
using FurnitureShop.BL.Dtos.ProductImageDto;
using FurnitureShop.Core.Entities;

namespace FurnitureShop.BL.Profiles;

public class ProductImageProfile : Profile
{
    public ProductImageProfile()
    {
        CreateMap<ProductImageCreateDto, ProductImage>()
            .ForMember(x=> x.CreatedTime, y=> y.MapFrom(x=> DateTime.UtcNow));

        CreateMap<ProductImage, ProductImageGetDto>();
        CreateMap<ProductImageUpdateDto, ProductImage>()
            .ForMember(x=> x.UpdatedTime, y=> y.MapFrom(x=> DateTime.UtcNow))
            .ForMember(x=> x.IsUpdated, y=> y.MapFrom(x=> true))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        ; 
    }
}