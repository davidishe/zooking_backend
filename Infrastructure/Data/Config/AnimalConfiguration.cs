using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Models;

namespace Infrastructure.Data.Config
{
  public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
  {
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
      builder.Property(p => p.Id).IsRequired();
      builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
      builder.Property(p => p.Description).IsRequired();
      builder.Property(p => p.PictureUrl).IsRequired();
      builder.Property(p => p.GuId);
      builder.Property(p => p.EnrolledDate).IsRequired();
      builder.HasOne(b => b.Type).WithMany().HasForeignKey(p => p.AnimalTypeId);
      builder.HasOne(b => b.Region).WithMany().HasForeignKey(p => p.RegionId);

    }
  }
}