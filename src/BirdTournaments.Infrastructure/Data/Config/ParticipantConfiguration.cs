using System;
using System.Collections.Generic;
using BirdTournaments.Core.ParticipantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BirdTournaments.Infrastructure.Data.Config;
public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
  public void Configure(EntityTypeBuilder<Participant> builder)
  {
    builder.Property(p => p.EloGain).IsRequired();
    builder.Property(p => p.Status)
      .HasConversion(
        s => s.Value,
        s => ParticipantStatus.FromValue(s))
      .IsRequired();

    builder.HasOne(p => p.BirdOwner).WithMany().IsRequired();
    builder.HasOne(p => p.Bird).WithMany().IsRequired();




  }
}
