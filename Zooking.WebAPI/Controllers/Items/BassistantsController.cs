using AutoMapper;
using Core.Dtos;
using Core.Models;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using WebAPI.Controllers;
using Zooking.Infrastructure.Database;

namespace Zooking.WebAPI.Controllers
{

  [AllowAnonymous]
  public class BassistantsController : BaseMapFilterController<Assistant, AssistantDto>
  {

    public BassistantsController(
      IDbRepository<Assistant> context,
      ILogger<BassistantsController> logger,
      IMapper mapper
    ) : base(context, logger, mapper)
    {
    }
  }
}