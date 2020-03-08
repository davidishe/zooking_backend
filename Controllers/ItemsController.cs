using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyAppBack.Data;
using MyAppBack.Models;
using QRCoder;

namespace MyAppBack.Controllers
{
  [Authorize]
  [Route("api")]
  [ApiController]
  public class ItemController : ControllerBase
  {
    private readonly DataDbContext _context;
    private readonly IQrRepository _qr;

    private readonly AppSettings _settings;

    public ItemController(DataDbContext context, IQrRepository qr, IOptions<AppSettings> settings)
    {
      _context = context;
      _qr = qr;
      _settings = settings.Value;
    }



    [AllowAnonymous]
    [Route("get-items/")]
    [HttpGet]
    public async Task<IActionResult> GetItems()
    {

      var v = await _context.Items.ToListAsync();
      if (v != null)
      {
        return Ok(v);
      }
      else
      {
        return Ok("Not found");
      }
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("create-item/")]
    public async Task<ActionResult> CreateItem(Item item)
    {
      item = LinkShortener(item);
      Console.WriteLine("This is item: " + item.EnrolledDate);
      item.Counter = 0;
      item.QrPath = _qr.CreateQrCode(item.ShortLink);

      await _context.Items.AddAsync(item);
      _context.SaveChanges();

      return Ok(item);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("get-item/")]
    public IActionResult GetItemById([FromQuery(Name = "id")] int id)
    {
      return Ok(_context.Items.Where(x => x.ItemId == id).FirstOrDefault());
    }

    [AllowAnonymous]
    [HttpDelete]
    [Route("delete-item/")]
    public bool DeleteItem([FromQuery(Name = "id")] int id)
    {
      Item item = _context.Items.Where(x => x.ItemId == id).FirstOrDefault();
      _context.Items.Remove(item);
      _context.SaveChanges();
      return _context.SaveChanges() > 0 ? true : false;
    }

    [AllowAnonymous]
    [Route("update-item/")]
    public bool UpdateItem([FromBody] Item item)
    {
      Item existingItem = _context.Items.Where(x => x.ItemId == item.ItemId).FirstOrDefault();
      if (existingItem.ItemId > 0)
      {
        existingItem.Link = item.Link;
        existingItem.EnrolledDate = item.EnrolledDate;
      }
      _context.Entry(existingItem).State = EntityState.Modified;
      return _context.SaveChanges() > 0 ? true : false;
    }

    public string Token { get; set; }
    // method calculate Token & Short Link and return it with item object
    public Item LinkShortener(Item item)
    {

      string domain = _settings.ServiceDomainUrl;

      Token = GenerateToken();
      item.Token = Token;
      item.ShortLink = domain + "/rd/rd/" + Token;
      return item;
    }

    // method which generate token
    private string GenerateToken()
    {
      string urlsafe = string.Empty;
      Enumerable.Range(48, 75)
              .Where(i => i < 58 || i > 64 && i < 91 || i > 96)
              .OrderBy(o => new Random().Next())
              .ToList()
              .ForEach(i => urlsafe += Convert.ToChar(i)); // Store each char into urlsafe
      Token = urlsafe.Substring(new Random().Next(0, urlsafe.Length), new Random().Next(2, 6));
      return Token;
    }

    // private string GenerateQr(string qrText)
    // {
    //   return "ffff";
    // }

  }



}
