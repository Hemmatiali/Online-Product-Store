using AutoMapper;
using CodeChallengeProject.Application.ViewModels.Product;
using CodeChallengeProject.Domain.Entities;
using CodeChallengeProject.Domain.ValueObjects;

namespace CodeChallengeProject.Application.Mapping;

/// <summary>
///     Represents a mapping profile for product-related classes.
/// </summary>
public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        //CreateMap<Product, ProductDto>();
        //CreateMap<UpdateProductViewModel, Product>();
        CreateMap<CreateProductViewModel, Product>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => new ProductTitleValueObject(src.Title)))
            .ForMember(dest => dest.InventoryCount, opt => opt.MapFrom(src => new InventoryCountValueObject(0))) // Assuming initial inventory is 0
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new PriceValueObject(src.Price)))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => new DiscountValueObject(src.Discount)));
    }
}