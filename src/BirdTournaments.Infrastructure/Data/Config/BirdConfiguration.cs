using BirdTournaments.Core.BirdAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BirdTournaments.Infrastructure.Data.Config;
public class BirdConfiguration : IEntityTypeConfiguration<Bird>
{
  public void Configure(EntityTypeBuilder<Bird> builder)
  {
    builder.Property(b => b.OwnerId).IsRequired(); 
    builder.Property(b => b.Elo).IsRequired();
    builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
    builder.Property(b => b.Image).HasMaxLength(100).IsRequired();

    builder.Metadata.FindNavigation(nameof(Bird.Rank))?.SetPropertyAccessMode(PropertyAccessMode.Field);
    builder.Metadata.FindNavigation(nameof(Bird.BirdType))?.SetPropertyAccessMode(PropertyAccessMode.Field);
  
  }
}
