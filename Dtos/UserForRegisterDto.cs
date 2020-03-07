using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyAppBack.Models;

namespace MyAppBack.Dtos
{
  public class UserForRegisterDto
  {

    [Required]
    // [StringLength(100, MinimumLength = 4, ErrorMessage = "Логин должен быть от 4 до 100 символов")]
    public string Username { get; set; }

    [Required]
    // [StringLength(100, MinimumLength = 4, ErrorMessage = "Пароль должен быть от 4 до 100 символов")]
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Item> UserLinks { get; set; }
  }
}