using System.Security.Claims;
using AutoMapper;
using Core.Dtos;
using Core.Identity;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;


namespace WebAPI.Helpers
{
  public class UserNameResolver : IValueResolver<Animal, AnimalToReturnDto, string>
  {
    public UserNameResolver()
    {
    }

    private readonly IConfiguration _config;
    private readonly UserManager<HavenAppUser> _userManager;


    public UserNameResolver(
      UserManager<HavenAppUser> userManager,
      IConfiguration config)
    {
      _config = config;
      _userManager = userManager;
    }

    public string Resolve(Animal source, AnimalToReturnDto destination, string destMember, ResolutionContext context)
    {
      var user = _userManager.FindByIdAsync("1").Result;
      var name = user.DisplayName;
      return name;
    }
  }

}