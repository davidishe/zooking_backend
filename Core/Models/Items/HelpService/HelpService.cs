using System;

namespace Core.Models
{
  public class HelpService : BaseEntity
  {
    public HelpService()
    {
    }

    public HelpService(string description, int helpServiceTypeId, HelpServiceType? type, int regionId, Region? region)
    {
      Description = description;
      HelpServiceTypeId = helpServiceTypeId;
      Type = type;
      RegionId = regionId;
      Region = region;
      GuId = GetGuId();
    }

    public string Description { get; set; }
    public HelpServiceType Type { get; set; }
    public int HelpServiceTypeId { get; set; }
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
