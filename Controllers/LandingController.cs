using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAppBack.Data;
using MyAppBack.Models;

namespace MyAppBack.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class LandingController : ControllerBase
  {

    private readonly DataDbContext _context;

    public LandingController(DataDbContext context)
    {
      _context = context;
    }

    [AllowAnonymous]
    [Route("get-items")]
    [HttpGet]
    public async Task<IActionResult> GetItems()
    {
      var response = await _context.MainPageProducts.ToListAsync();
      if (response != null)
      {
        return Ok(response);
      }
      else
      {
        return Ok("Not found");
      }
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("create-item")]
    public async Task<ActionResult> CreateItem(MainPageProduct mainPageProduct)
    {
      await _context.MainPageProducts.AddAsync(mainPageProduct);
      _context.SaveChanges();

      var products = await _context.MainPageProducts.ToListAsync();
      return Ok(products);
    }

    [AllowAnonymous]
    [HttpDelete]
    [Route("delete-item")]
    public async Task<IActionResult> DeleteItem([FromQuery(Name = "productId")] int productId)
    {
      MainPageProduct product = _context.MainPageProducts.Where(x => x.MainPageProductId == productId).FirstOrDefault();
      _context.MainPageProducts.Remove(product);
      _context.SaveChanges();

      var products = await _context.MainPageProducts.ToListAsync();
      return Ok(products);
    }

  }



}

