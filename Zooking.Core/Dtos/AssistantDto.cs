using System.Collections.Generic;
using Core.Models;
using Zooking.Core.Dtos;
using Zooking.Core.Models.Members;

namespace Core.Dtos
{
  public class AssistantDto : BaseDto
  {
    public string Name { get; set; }
    public bool IsEnabled { get; set; }
    public double? Rating { get; set; }
    public string MainPhoto { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string House { get; set; }
    public string ZipCode { get; set; }
    public virtual ICollection<MemberChat>? MemberChats { get; set; }
  };
}

