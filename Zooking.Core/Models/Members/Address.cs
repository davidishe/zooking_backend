using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Models
{

  [Owned]
  public class Address
  {
    public string Street { get; set; }
    public string City { get; set; }
    public string House { get; set; }
    public string ZipCode { get; set; }
    public int AssistantId { get; set; }

    [ForeignKey("AssistantId")]
    public Assistant Assistant { get; set; }

  }
}