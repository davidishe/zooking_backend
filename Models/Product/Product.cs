using System;

namespace MyAppBack.Models
{
  public class Product : BaseEntity
  {
    public string Name { get; set; }
    public int ProductPrice { get; set; }
    public string PictureUrl { get; set; }
    public string? Description { get; set; }
    public ProductType? ProductType { get; set; }
    public int? ProductTypeId { get; set; }
    public ProductRegion? ProductRegion { get; set; }
    public int? ProductRegionId { get; set; }
    public int? Quantity { get; set; }
    public bool? ProductIsSelected { get; set; }
    public DateTime? EnrolledDate { get; set; } = DateTime.Now;
    public int? GuId { get; set; }

  }
}
