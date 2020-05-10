using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyAppBack.Models;
using MyAppBack.Models.OrderAggregate;

namespace MyAppBack.Data.SeedData
{
  public class StoreContextSeed
  {
    private readonly IOptions<AppSettings> _settings;

    public static async Task SeedDataAsync(DataDbContext context, ILoggerFactory loggerFactory)
    {
      try
      {
        if (!context.ProductRegions.Any())
        {
          var productsRegionsData = File.ReadAllText("./Data/SeedData/Source/regions.json");
          var productsRegions = JsonSerializer.Deserialize<List<ProductRegion>>(productsRegionsData);
          foreach (var item in productsRegions)
          {
            context.ProductRegions.Add(item);
          }
          await context.SaveChangesAsync();
        }

        if (!context.ProductTypes.Any())
        {
          var productsTypesData = File.ReadAllText("./Data/SeedData/Source/types.json");
          var productsTypes = JsonSerializer.Deserialize<List<ProductType>>(productsTypesData);
          foreach (var item in productsTypes)
          {
            context.ProductTypes.Add(item);
          }
          await context.SaveChangesAsync();
        }

        if (!context.Products.Any())
        {
          var productsData = File.ReadAllText("./Data/SeedData/Source/products.json");
          var products = JsonSerializer.Deserialize<List<Product>>(productsData);


          foreach (var item in products)
          {
            // item.PictureUrl = "http://localhost:5000/" + item.PictureUrl;
            context.Products.Add(item);
          }
          await context.SaveChangesAsync();
        }

        if (!context.DeliveryMethods.Any())
        {
          var json = File.ReadAllText("./Data/SeedData/Source/delivery.json");
          var data = JsonSerializer.Deserialize<List<DeliveryMethod>>(json);
          foreach (var item in data)
          {
            context.DeliveryMethods.Add(item);
          }
          await context.SaveChangesAsync();
        }

        if (!context.DeliveryMethods.Any())
        {
          var json = File.ReadAllText("./Data/SeedData/Source/delivery.json");
          var data = JsonSerializer.Deserialize<List<DeliveryMethod>>(json);
          foreach (var item in data)
          {
            context.DeliveryMethods.Add(item);
          }
          await context.SaveChangesAsync();
        }


      }
      catch (Exception ex)
      {
        var logger = loggerFactory.CreateLogger<StoreContextSeed>();
        logger.LogError(ex.Message);
      }
    }


  }
}