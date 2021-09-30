using System.Threading.Tasks;
using Core.Identity;

namespace Zooking.Identity
{
  public interface ITokenService
  {
    Task<string> CreateToken(HavenAppUser user);
  }
}