using System.Threading.Tasks;
using Core.External.Contracts;
using Core.Models.Contracts;

namespace Zooking.Identity.Services
{
  public interface IFacebookAuthService
  {
    Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
    Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
    Task<AccesssTokenResponse> GetAccessTokenAsync(string code);

  }
}