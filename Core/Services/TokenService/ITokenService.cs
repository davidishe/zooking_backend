using System.Threading.Tasks;
using Core.Identity;

namespace Core.Services.TokenService
{
  public interface ITokenService
  {
    Task<string> CreateToken(HavenAppUser user);
  }
}