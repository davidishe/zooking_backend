using AutoMapper;
using MyAppBack.Dtos;
using MyAppBack.Models;

namespace MyAppBack.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<User, UserForListDto>();
      CreateMap<User, UserForDetailedDto>();
    }
  }
}