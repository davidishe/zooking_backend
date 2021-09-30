using AutoMapper;
using Core.Dtos;
using Core.Identity;
using Core.Models;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Helpers
{
  public class UrlPictureReslover : IValueResolver<Assistant, AssistantDto, string>
  {
    public UrlPictureReslover()
    {
    }

    private readonly IConfiguration _config;

    public UrlPictureReslover(IConfiguration config)
    {
      _config = config;
    }

    public string Resolve(Assistant source, AssistantDto destination, string destMember, ResolutionContext context)
    {
      if (!string.IsNullOrEmpty(source.MainPhoto))
      {
        return _config.GetSection("ApiUrl").Value + "Assets/Images/" + source.MainPhoto;
      }
      return null;
    }
  }

}