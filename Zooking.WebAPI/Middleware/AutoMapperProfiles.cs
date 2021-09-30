using AutoMapper;
using Core.Dtos;
using Core.Identity;
using Core.Models;

namespace WebAPI.Helpers
{
  public class AutoMapperProfiles : Profile
  {

    public AutoMapperProfiles()
    {
      CreateMap<HavenAppUser, UserToReturnDto>()
        .ForMember(d => d.PictureUrl, m => m.MapFrom(s => s.PictureUrl))
        .ForMember(d => d.UserPosition, m => m.MapFrom(s => s.UserPosition.Name))
        .ForMember(d => d.PictureUrl, m => m.MapFrom<UrlPictureForUserReslover>())
        .ForMember(d => d.BankOffice, m => m.MapFrom<UserBankOfficeResolver>())
        .ForMember(d => d.UserRoles, m => m.MapFrom<UserRolesReslover>());

      CreateMap<Assistant, AssistantDto>()
        .ForMember(d => d.MainPhoto, m => m.MapFrom<UrlPictureReslover>());


      CreateMap<Assistant, AssistantDto>()
        .ForMember(d => d.MainPhoto, m => m.MapFrom<UrlPictureReslover>());


      CreateMap<Item, ItemDto>()
        .ForMember(d => d.AppUser, m => m.MapFrom<UserResolver>())
        .ForMember(d => d.ItemType, m => m.MapFrom(s => s.ItemType.Name));

    }



  }
}