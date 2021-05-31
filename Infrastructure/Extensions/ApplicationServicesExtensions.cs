using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Core.Services;
using Core.Services.ResponseCacheService;
using Core.Services.TokenService;
using Infrastructure.Data.Repos.GenericRepository;
using Infrastructure.Data.UnitOfWork;
using Infrastructure.Data.Repos;
using Infrastructure.Errors;

namespace Core.Extensions
{
  public static class ApplicationServicesExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      services.AddSingleton<IResponseCacheService, ResponseCacheService>();
      services.AddScoped<ITokenService, TokenService>();
      services.AddScoped<IAnimalRepository, AnimalRepository>();
      services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
      services.AddScoped<IUnitOfWork, UnitOfWork>();


      services.Configure<ApiBehaviorOptions>(options =>
      {
        options.InvalidModelStateResponseFactory = actionContext =>
        {
          var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage).ToArray();

          var errorResponse = new ApiValidationErrorResponse()
          {
            Errors = errors
          };

          return new BadRequestObjectResult(errorResponse);

        };

      });

      return services;

    }
  }
}