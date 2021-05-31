using System.Collections.Generic;
using Core.Models.Identity;

namespace Core.Dtos
{
  public class UserToReturnDto
  {
    public int? Id { get; set; }
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string? PictureUrl { get; set; }

    public string? UserDescription { get; set; }
    public bool? IsOnboarded { get; set; }
    public AddressDto? Address { get; set; }
    public string? Token { get; set; }
    public IList<string>? UserRoles { get; set; }


  }
}