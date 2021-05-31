using System;

namespace Core.Dtos
{
  public class AnimalToReturnDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string PictureUrl { get; set; }
    public string? Description { get; set; }
    public string Type { get; set; }
    public int TypeId { get; set; }
    public string Region { get; set; }
    public int RegionId { get; set; }
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public bool? ProductIsSelected { get; set; }
    public DateTime? EnrolledDate { get; set; }
    public int? GuId { get; set; }
    public int? OrderId { get; set; }
    public bool IsSale { get; set; }

  };
}