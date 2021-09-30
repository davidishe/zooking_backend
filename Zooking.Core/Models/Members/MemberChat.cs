using Core.Models;

namespace Zooking.Core.Models.Members
{
  public class MemberChat : BaseEntity
  {

    public int ChatId { get; set; }
    public Chat? Chat { get; set; }
    public int AssistantId { get; set; }
    public Assistant? Assistant { get; set; }
  }
}