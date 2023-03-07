using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdTournaments.Core.ParticipantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BirdTournaments.Infrastructure.Data.Config;
public class PlaceConfiguration : IEntityTypeConfiguration<Place>
{
  public void Configure(EntityTypeBuilder<Place> builder)
  {
    builder.Property(p => p.Address).HasMaxLength(100).IsRequired();

    builder.HasOne(p => p.Region).WithMany();
  }
}
