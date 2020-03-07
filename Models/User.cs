using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppBack.Models
{
  [Table("Users")]
  public class User
  {
    [Key]
    public int UserId { get; set; }
    [EmailAddress]
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Item> Items { get; set; }
  }
}