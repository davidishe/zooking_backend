using AutoMapper;
using MyAppBack.Dtos;
using MyAppBack.Models;
using MyAppBack.Models.OrderAggregate;

namespace MyAppBack.Helpers
{
  public class AutoMapperProfiles : Profile
  {

    public AutoMapperProfiles()
    {
      CreateMap<Product, ProductToReturnDto>()
        .ForMember(d => d.ProductType, m => m.MapFrom(s => s.ProductType.Name))
        .ForMember(d => d.ProductRegion, m => m.MapFrom(s => s.ProductRegion.Name))
        .ForMember(d => d.PictureUrl, m => m.MapFrom<UrlResolver>());

      CreateMap<MyAppBack.Identity.Address, AddressDto>().ReverseMap();
      CreateMap<BasketDto, Basket>();
      CreateMap<BasketItemDto, BasketItem>();
      CreateMap<AddressDto, MyAppBack.Models.OrderAggregate.Address>();
      CreateMap<Order, OrderToReturnDto>()
        .ForMember(d => d.DeliveryMethod, m => m.MapFrom(s => s.DeliveryMethod.ShortName))
        .ForMember(d => d.DeliveryPrice, m => m.MapFrom(s => s.DeliveryMethod.Price));

      CreateMap<OrderItem, OrderItemDto>()
        .ForMember(d => d.ProductId, m => m.MapFrom(s => s.ItemOrdered.ProductItemId))
        .ForMember(d => d.ProductName, m => m.MapFrom(s => s.ItemOrdered.ProductName))
        .ForMember(d => d.PictureUrl, m => m.MapFrom<OrderItemUrlResolver>());


    }
  }
}