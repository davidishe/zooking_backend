using System;
using Core.Models;

namespace Core.Dtos
{
  public class ShelterToCreate
  {
    public int? Id { get; set; }
    public string Name { get; set; }
    public int? AnimalCount { get; set; }
    public string? PictureUrl { get; set; }
    public string? Description { get; set; }
    public int? RegionId { get; set; }
    public int? TypeId { get; set; }
    public DateTime? EnrolledDate { get; set; } = DateTime.Now;
    public int? GuId { get; set; }
    public bool IsSale { get; set; }
  }
}