using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Core.Dtos;
using Core.Identity;
using Core.Models;
using Core.Models.Identity;
using Core.Helpers;
using Core.Services.TokenService;

namespace WebAPI.Helpers
{
  public class AutoMapperProfiles : Profile
  {

    private readonly UserManager<HavenAppUser> _userManager;
    private readonly ITokenService _tokenService;



    public AutoMapperProfiles(
      UserManager<HavenAppUser> userManager,
      ITokenService tokenService
      )
    {
      _userManager = userManager;
      _tokenService = tokenService;
    }

    public AutoMapperProfiles()
    {
      CreateMap<HavenAppUser, UserToReturnDto>()
        .ForMember(d => d.PictureUrl, m => m.MapFrom(s => s.PictureUrl))
        .ForMember(d => d.PictureUrl, m => m.MapFrom<UrlPictureForUserReslover>())
        .ForMember(d => d.UserRoles, m => m.MapFrom<UserRolesReslover>());

      CreateMap<Animal, AnimalToReturnDto>()
        .ForMember(d => d.Type, m => m.MapFrom(s => s.Type.Name))
        .ForMember(d => d.Region, m => m.MapFrom(s => s.Region.Name))
        .ForMember(d => d.UserName, m => m.MapFrom<UserNameResolver>())
        .ForMember(d => d.PictureUrl, m => m.MapFrom<UrlAnimalResolver>());

      CreateMap<Core.Identity.Address, AddressDto>().ReverseMap();

      CreateMap<Shelter, ShelterToReturnDto>()
        .ForMember(d => d.Region, m => m.MapFrom(s => s.Region.Name))
        .ForMember(d => d.PictureUrl, m => m.MapFrom<UrlShelterResolver>());

    }



  }
}