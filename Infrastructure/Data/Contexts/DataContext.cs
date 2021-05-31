using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Contexts
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Shelter> Shelters { get; set; }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<AnimalType> AnimalTypes { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<HelpService> HelpServices { get; set; }
    public DbSet<HelpServiceType> HelpServiceTypes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

  }

}
