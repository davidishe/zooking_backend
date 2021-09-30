using System.Threading.Tasks;
using Core.Domain;
using Core.Models.Contracts;

namespace Zooking.Identity.Services
{
  public interface IRoleManagerService
  {

    Task<bool> ChangeUserRoles(string[] roles, string userId);

  }
}