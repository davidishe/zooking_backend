using Microsoft.Extensions.Options;
using MyAppBack.Models;

namespace MyAppBack.Helpers
{
  public class ServerUrlHelper
  {

    private readonly IOptions<AppSettings> _settings;

    public ServerUrlHelper(IOptions<AppSettings> settings)
    {
      _settings = settings;
    }

    private string GetServerUrl(string prictureUrl)
    {
      return _settings.Value.ServiceDomainUrl + prictureUrl;
    }

  }
}