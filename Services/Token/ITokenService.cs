using MyAppBack.Identity;

namespace MyAppBack.Services.Token
{
  public interface ITokenService
  {
    string CreateToken(AppUser user);
  }
}