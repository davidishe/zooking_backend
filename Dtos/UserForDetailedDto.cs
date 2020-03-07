using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyAppBack.Models;

namespace MyAppBack.Dtos
{
  public class UserForDetailedDto
  {
    [Key]
    public int UserId { get; set; }
    [EmailAddress]
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Item> Items { get; set; }
  }
}