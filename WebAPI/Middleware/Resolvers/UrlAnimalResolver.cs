using AutoMapper;
using Core.Dtos;
using Core.Models;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Helpers
{
  public class UrlAnimalResolver : IValueResolver<Animal, AnimalToReturnDto, string>
  {
    public UrlAnimalResolver()
    {
    }

    private readonly IConfiguration _config;

    public UrlAnimalResolver(IConfiguration config)
    {
      _config = config;
    }

    public string Resolve(Animal source, AnimalToReturnDto destination, string destMember, ResolutionContext context)
    {
      if (!string.IsNullOrEmpty(source.PictureUrl))
      {
        return _config.GetSection("ApiUrl").Value + "Assets/Images/Animals/" + source.PictureUrl;
      }
      return null;
    }
  }

}