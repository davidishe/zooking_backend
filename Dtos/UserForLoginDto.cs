using System.ComponentModel.DataAnnotations;

namespace MyAppBack.Dtos
{
  public class UserForLoginDto
  {

    [Key]
    public int UserId { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Логин должен быть от 4 до 100 символов")]
    public string username { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Пароль должен быть от 4 до 100 символов")]
    public string password { get; set; }
  }
}