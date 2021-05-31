using System;
using System.Threading.Tasks;

namespace Core.Services.ResponseCacheService
{
  public interface IResponseCacheService
  {
    Task CacheResponseService(string cacheKey, object response, TimeSpan timeToLive);
    Task<string> GetCacheResponseAsync(string cacheKey);
  }
}