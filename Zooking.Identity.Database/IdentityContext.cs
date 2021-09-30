using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Core.Domain;
using Core.Identity;
using Core.Models.Identity;

namespace Zooking.Identity.Database
{
  public class IdentityContext : IdentityDbContext<HavenAppUser, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
  {
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserPosition> UserPosition { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<UserRole>(userRole =>
      {
        userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

        userRole.HasOne(ur => ur.Role)
          .WithMany(r => r.UserRoles)
          .HasForeignKey(ur => ur.RoleId)
          .IsRequired();

        userRole.HasOne(ur => ur.User)
          .WithMany(r => r.UserRoles)
          .HasForeignKey(ur => ur.UserId)
          .IsRequired();


      });



    }


  }
}