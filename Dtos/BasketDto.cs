using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAppBack.Dtos
{
  public class BasketDto
  {
    [Required]
    public string Id { get; set; }
    public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
  }
}