using System.Collections.Generic;

namespace MyAppBack.Models
{
  public class Basket
  {
    public Basket(string id)
    {
      Id = id;
    }

    public Basket()
    {
    }

    public string Id { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();

  }
}