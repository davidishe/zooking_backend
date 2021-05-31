using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Core.Identity;
using Core.Models.Identity;

namespace Infrastructure.Data.SeedData
{
  public class IdentityContextSeed
  {
    public static async Task SeedUsersAsync(UserManager<HavenAppUser> userManager, RoleManager<Role> roleManager, ILoggerFactory loggerFactory)
    {
      var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

      try
      {


        if (!userManager.Users.Any())
        {

          var roles = new List<Role>()
          {
            new Role {Name = "User"}, // обычный посетитель сайта
            new Role {Name = "Admin", RoleName = "Администратор"}, // администратор

            new Role {Name = "Curator", RoleName = "Куратор"}, // куратор животных, создает карточки животных, которых нашел и за которыми ухаживает
            new Role {Name = "Volonter", RoleName = "Волонтер"}, // помощник, помогает чем может

            new Role {Name = "ShelterOwner", RoleName = "Представитель приюта"}, // помощник, помогает чем может
            new Role {Name = "Feeder", RoleName = "Представитель компании"}, // представитель организации, которая помогает
          };


          foreach (var role in roles)
          {
            await roleManager.CreateAsync(role);
          }

          var itemsData = File.ReadAllText(path + @"/Data/SeedData/Source/users.json");
          var users = JsonSerializer.Deserialize<List<HavenAppUser>>(itemsData);
          foreach (var user in users)
          {
            var result = userManager.CreateAsync(user, "Pa$$w0rd").Result;
          }

          var admin = userManager.FindByNameAsync("admin@admin.ru").Result;
          await userManager.AddToRolesAsync(admin, new[] { "Admin", "User" });

          var feeder = userManager.FindByNameAsync("feeder@feeder.ru").Result;
          await userManager.AddToRolesAsync(feeder, new[] { "Feeder", "User" });

          var volonter = userManager.FindByNameAsync("volonter@volonter.ru").Result;
          await userManager.AddToRolesAsync(volonter, new[] { "Volonter", "User" });

          var curator = userManager.FindByNameAsync("curator@curator.ru").Result;
          await userManager.AddToRolesAsync(curator, new[] { "Curator", "User" });

        }




      }
      catch (Exception ex)
      {
        var logger = loggerFactory.CreateLogger<IdentityContextSeed>();
        logger.LogError(ex.Message);
      }
    }
  }
}