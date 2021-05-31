using System;

namespace Core.Models
{
  public class Feeder : BaseEntity
  {
    public Feeder()
    {
    }


    public string Name { get; set; }
    public string? PictureUrl { get; set; }
    public string? Description { get; set; }
    public DateTime? EnrolledDate { get; set; } = DateTime.Now;

  }



}
