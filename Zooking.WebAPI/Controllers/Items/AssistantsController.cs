using AutoMapper;
using Core.Dtos;
using Core.Models;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using WebAPI.Controllers;

namespace Zooking.WebAPI.Controllers
{

  [AllowAnonymous]
  public class AssistantsController : BaseMapController<Assistant, AssistantDto>
  {

    public AssistantsController(
      IGenericRepository<Assistant> context,
      ILogger<AssistantsController> logger,
      IMapper mapper
    ) : base(context, logger, mapper)
    {
    }
  }
}