using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Zooking.Core.Models.Members;

namespace Core.Models
{
  public class Assistant : BaseEntity
  {
    public Assistant()
    {
    }

    public Assistant(string name, bool isEnabled, string mainPhoto, double rating, Address address)
    {
      Name = name;
      IsEnabled = isEnabled;
      MainPhoto = mainPhoto;
      Rating = rating;
      Address = address;
    }

    public string Name { get; set; }
    public bool IsEnabled { get; set; }
    public double? Rating { get; set; }
    public Address Address { get; set; }
    public string MainPhoto { get; set; }
    public virtual ICollection<MemberChat>? MemberChats { get; set; }

  }
}