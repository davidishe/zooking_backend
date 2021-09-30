using System;
using System.Collections.Generic;
using Zooking.Core.Models.Members;

namespace Core.Models
{
  public class Assistant : BaseEntity
  {
    public Assistant()
    {
    }

    public Assistant(string name, bool isEnabled, string mainPhoto)
    {
      Name = name;
      IsEnabled = isEnabled;
      MainPhoto = mainPhoto;
    }

    public string Name { get; set; }
    public bool IsEnabled { get; set; }
    public string MainPhoto { get; set; }
    public virtual ICollection<MemberChat>? MemberChats { get; set; }

  }
}