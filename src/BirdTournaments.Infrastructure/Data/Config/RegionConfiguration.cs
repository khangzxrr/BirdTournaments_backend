using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdTournaments.Core.ParticipantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BirdTournaments.Infrastructure.Data.Config;
public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
  public void Configure(EntityTypeBuilder<Region> builder)
  {
    builder.Property(r => r.Name).HasMaxLength(100).IsRequired();
  }
}
