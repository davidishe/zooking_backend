using System.IO;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MyAppBack.Data;
using MyAppBack.Models;

namespace MyAppBack
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public void ConfigureDevelopmentServices(IServiceCollection services)
    {
      services.AddDbContext<DataDbContext>(options =>
      options.UseSqlite(
          Configuration.GetConnectionString("DefaultConnection")));


      ConfigureServices(services);
    }

    public void ConfigureProductionServices(IServiceCollection services)
    {
      services.AddDbContext<DataDbContext>(options =>
        options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));

      ConfigureServices(services);
    }

    public void ConfigureServices(IServiceCollection services)
    {

      var appSettings =
          Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettings);

      services.AddControllers()
        .AddNewtonsoftJson(opt =>
        {
          opt.SerializerSettings.ReferenceLoopHandling =
          Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

      services.AddCors();

      services.AddAutoMapper(typeof(UserRepository).Assembly);
      services.AddScoped<IAuthRepository, AuthRepository>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IQrRepository, QrRepository>();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
            .GetBytes(Configuration.GetSection("AppKeys:Token").Value)),
          ValidateAudience = false,
          ValidateIssuer = false
        };
      });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapFallbackToController("Index", "Fallback");
      });
    }
  }
}
