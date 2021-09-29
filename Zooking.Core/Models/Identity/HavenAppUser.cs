using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Core.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models;

namespace Core.Identity
{
  public class HavenAppUser : IdentityUser<int>
  {
    public string DisplayName { get; set; }
    public string? PictureUrl { get; set; }
    public string? UserDescription { get; set; }
    public virtual Address? Address { get; set; }

    public UserPosition UserPosition { get; set; }
    public int UserPositionId { get; set; }
    public int BankOfficeId { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }

  }
}