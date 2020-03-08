using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyAppBack.Models
{
  [Table("Items")]
  public class Item
  {
    [Key]
    public int ItemId { get; set; }
    [Required]
    public string Link { get; set; }
    public string? ShortLink { get; set; }
    public string Token { get; set; }
    [Required]
    public DateTime EnrolledDate { get; set; }
    public int? Counter { get; set; }
    public string? QrPath { get; set; }
    public int UserId { get; set; }
  }

}

