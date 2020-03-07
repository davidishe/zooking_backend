using MyAppBack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MyAppBack.Data
{
  public class DataDbContext : DbContext
  {
    public DataDbContext(
        DbContextOptions<DataDbContext> options) : base(options) { }

    public DbSet<Item> Items { get; set; }
    public DbSet<User> Users { get; set; }
  }

}
