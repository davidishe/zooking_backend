using System;

namespace Core.Dtos
{
  public class ShelterToReturnDto
  {
    public int Id { get; set; }
    public string? Description { get; set; }
    public string Name { get; set; }
    public int? AnimalCount { get; set; }
    public string? PictureUrl { get; set; }
    public string Region { get; set; }
    public int ShelterRegionId { get; set; }
    public DateTime? EnrolledDate { get; set; }
    public int GuId { get; set; }
    public bool IsSale { get; set; }

  };
}