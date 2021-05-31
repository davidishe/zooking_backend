using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Core.Models.Identity;

namespace Core.Identity
{
  public class HavenAppUser : IdentityUser<int>
  {
    public string DisplayName { get; set; }
    public string? PictureUrl { get; set; }
    public string? UserDescription { get; set; }
    public bool? IsOnboarded { get; set; }
    public virtual Address? Address { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }

  }
}