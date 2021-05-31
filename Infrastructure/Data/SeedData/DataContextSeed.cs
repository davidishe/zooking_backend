using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Infrastructure.Data.Contexts;
using Core.Models;

namespace Infrastructure.Data.SeedData
{
  public class DataContextSeed
  {
    public static async Task SeedDataAsync(DataContext context, ILoggerFactory loggerFactory)
    {
      try
      {

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (!context.Regions.Any())
        {
          var productsRegionsData = File.ReadAllText(path + @"/Data/SeedData/Source/regions.json");
          var productsRegions = JsonSerializer.Deserialize<List<Region>>(productsRegionsData);
          foreach (var item in productsRegions)
          {
            context.Regions.Add(item);
          }
          await context.SaveChangesAsync();
        }


        if (!context.AnimalTypes.Any())
        {
          var itemsData = File.ReadAllText(path + @"/Data/SeedData/Source/anymaltypes.json");
          var items = JsonSerializer.Deserialize<List<AnimalType>>(itemsData);
          foreach (var item in items)
          {
            context.AnimalTypes.Add(item);
          }
          await context.SaveChangesAsync();
        }

        if (!context.HelpServiceTypes.Any())
        {
          var itemsData = File.ReadAllText(path + @"/Data/SeedData/Source/helpservicetypes.json");
          var items = JsonSerializer.Deserialize<List<HelpServiceType>>(itemsData);
          foreach (var item in items)
          {
            context.HelpServiceTypes.Add(item);
          }
          await context.SaveChangesAsync();
        }


        if (!context.HelpServices.Any())
        {
          var itemsData = File.ReadAllText(path + @"/Data/SeedData/Source/helpservices.json");
          var items = JsonSerializer.Deserialize<List<HelpService>>(itemsData);
          foreach (var item in items)
          {
            context.HelpServices.Add(item);
          }
          await context.SaveChangesAsync();
        }


        if (!context.Shelters.Any())
        {
          var itemsData = File.ReadAllText(path + @"/Data/SeedData/Source/shelters.json");
          var items = JsonSerializer.Deserialize<List<Shelter>>(itemsData);
          foreach (var item in items)
          {
            context.Shelters.Add(item);
          }
          await context.SaveChangesAsync();
        }


        if (!context.Animals.Any())
        {
          var productsData = File.ReadAllText(path + @"/Data/SeedData/Source/animals.json");
          var products = JsonSerializer.Deserialize<List<Animal>>(productsData);


          foreach (var item in products)
          {
            context.Animals.Add(item);
          }
          await context.SaveChangesAsync();
        }


      }
      catch (Exception ex)
      {
        var logger = loggerFactory.CreateLogger<DataContextSeed>();
        logger.LogError(ex.Message);
      }
    }


  }
}