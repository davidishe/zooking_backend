using Zooking.Core.Models.Members;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Zooking.Infrastructure.Database
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
      Database.EnsureCreated();
    }

    public DbSet<Office> BankOffices { get; set; }
    public DbSet<ItemType> ItemTypes { get; set; }
    public DbSet<ItemSubType> ItemSubTypes { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Assistant> Assistants { get; set; }
    // public DbSet<ItemChat> ItemChats { get; set; }
    // public DbSet<MemberChat> MemberChats { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      modelBuilder.Entity<ItemChat>()
          .HasKey(cs => new { cs.ChatId, cs.ItemId });

      modelBuilder.Entity<MemberChat>()
        .HasKey(cs => new { cs.ChatId, cs.AssistantId });

      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

  }

}
