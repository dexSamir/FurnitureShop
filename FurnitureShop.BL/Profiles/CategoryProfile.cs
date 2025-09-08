using AutoMapper;
using FurnitureShop.BL.Dtos.CategoryDtos;
using FurnitureShop.Core.Entities;

namespace FurnitureShop.BL.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryGetDto>();
        
        CreateMap<CategoryCreateDto, Category>()
            .ForMember(x => x.CreatedTime , y => y.MapFrom(z => DateTime.UtcNow)); 
        
        CreateMap<CategoryUpdateDto, Category>()
            .ForMember(x => x.UpdatedTime, y=> y.MapFrom(x=> DateTime.UtcNow))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}