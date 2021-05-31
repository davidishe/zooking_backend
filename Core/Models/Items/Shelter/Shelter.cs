using System;

namespace Core.Models
{
  public class Shelter : BaseEntity
  {
    public Shelter()
    {
    }

    public Shelter(string name, string pictureUrl, string? description, int regionId, Region? region, int? animalCount)
    {
      Name = name;
      PictureUrl = pictureUrl;
      Description = description;
      RegionId = regionId;
      Region = region;
      AnimalCount = animalCount;
      GuId = GetGuId();
    }

    public string Name { get; set; }
    public int? AnimalCount { get; set; }
    public string? PictureUrl { get; set; }
    public string? Description { get; set; }
    public Region Region { get; set; }
    public int RegionId { get; set; }
    public DateTime? EnrolledDate { get; set; } = DateTime.Now;

    private int GetGuId()
    {
      int i = Guid.NewGuid().GetHashCode();
      return i;
    }

  }



}
