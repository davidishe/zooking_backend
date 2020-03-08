using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAppBack.Data;
using MyAppBack.Models;

namespace MyAppBack.Controllers
{

  [Route("/[controller]")]
  public class RdController : ControllerBase
  {
    public readonly DataDbContext _context;
    public RdController(DataDbContext context)
    {
      _context = context;
    }

    [Route("rd/{token}")]
    [HttpGet]
    public async Task<IActionResult> RedirectMe([FromRoute] string token)
    {
      Item selectedItem = _context.Items.Where(x => x.Token == token).FirstOrDefault();
      await Task.Run(() => Counter(selectedItem.Token));
      return Redirect(selectedItem.Link);
    }

    public Item Counter(string token)
    {
      Thread.Sleep(1);
      var item = _context.Items.Where(x => x.Token == token).FirstOrDefault();
      if (item.Counter == null)
      {
        item.Counter = 1;
      }
      else
      {
        item.Counter = item.Counter + 1;
        UpdateItem(item);
      }
      return item;
    }

    public bool UpdateItem([FromBody] Item item)
    {
      Item existingItem = _context.Items.Where(x => x.Token == item.Token).FirstOrDefault();
      if (existingItem.ItemId > 0)
      {
        existingItem.Link = item.Link;
        existingItem.EnrolledDate = item.EnrolledDate;
      }
      _context.Entry(existingItem).State = EntityState.Modified;
      return _context.SaveChanges() > 0 ? true : false;
    }


  }


}
