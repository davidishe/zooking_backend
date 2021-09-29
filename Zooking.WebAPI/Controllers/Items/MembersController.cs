using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Database;
using Microsoft.Extensions.Logging;
using WebAPI.Controllers;

namespace Bot.WebAPI.Controllers
{

  [AllowAnonymous]
  public class MembersController : BaseController<Member>
  {

    public MembersController(
      IGenericRepository<Member> context,
      ILogger<MembersController> logger
    ) : base(context, logger)
    {
    }
  }
}

