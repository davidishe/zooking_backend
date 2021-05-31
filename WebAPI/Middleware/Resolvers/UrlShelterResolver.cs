using AutoMapper;
using Core.Dtos;
using Core.Models;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Helpers
{
  public class UrlShelterResolver : IValueResolver<Shelter, ShelterToReturnDto, string>
  {
    public UrlShelterResolver()
    {
    }

    private readonly IConfiguration _config;

    public UrlShelterResolver(IConfiguration config)
    {
      _config = config;
    }

    public string Resolve(Shelter source, ShelterToReturnDto destination, string destMember, ResolutionContext context)
    {
      if (!string.IsNullOrEmpty(source.PictureUrl))
      {
        return _config.GetSection("ApiUrl").Value + "Assets/Images/Shelters/" + source.PictureUrl;
      }
      return null;
    }
  }

}