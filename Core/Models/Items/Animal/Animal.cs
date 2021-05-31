using System;
using Core.Identity;

namespace Core.Models
{
  public class Animal : BaseEntity
  {
    public Animal()
    {
    }

    public Animal(string name, string pictureUrl, string? description, int animalTypeId, AnimalType? type, int regionId, Region? region)
    {
      Name = name;
      PictureUrl = pictureUrl;
      Description = description;
      AnimalTypeId = animalTypeId;
      Type = type;
      RegionId = regionId;
      Region = region;
      GuId = GetGuId();
      IsSale = false;
    }

    public string Name { get; set; }
    public string? PictureUrl { get; set; }
    public string? Description { get; set; }
    public AnimalType Type { get; set; }
    public int AnimalTypeId { get; set; }
    public Region Region { get; set; }
    public int RegionId { get; set; }
    public int UserId { get; set; }
    public DateTime? EnrolledDate { get; set; } = DateTime.Now;
    public bool? IsSale { get; set; }


    private int GetGuId()
    {
      int i = Guid.NewGuid().GetHashCode();
      return i;
    }

  }



}
