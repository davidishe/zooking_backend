using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppBack.Models
{

  [Table("MainPageProducts")]
  public class MainPageProduct
  {

    [Key]
    public int MainPageProductId { get; set; }
    public int GuId { get; set; }
    public string ProductTitle { get; set; }
    public string PictureUrl { get; set; }
    public int ProductPrice { get; set; }
    public DateTime? EnrolledDate { get; set; } = DateTime.Now;
    public bool? ProductIsSelected { get; set; }
    public ProductType? ProductType { get; set; }
    public int? ProductTypeId { get; set; }
    public ProductRegion? ProductRegion { get; set; }
    public int? ProductRegionId { get; set; }

  }
}





