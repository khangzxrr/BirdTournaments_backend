using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.Core.BirdOwnerAggregate;
using BirdTournaments.Core.ParticipantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BirdTournaments.Infrastructure.Data.Config;
public class CompetitionConfiguration : IEntityTypeConfiguration<Competition>
{
  public void Configure(EntityTypeBuilder<Competition> builder)
  {
    builder.Property(c => c.Date).IsRequired();
    
    builder.Property(c => c.Status)
      .HasConversion(
        s => s.Value,
        s => CompetitionStatus.FromValue(s)
      )
      .IsRequired();


    builder.HasMany(c => c.Participants).WithOne().OnDelete(DeleteBehavior.Restrict);


    builder.HasOne(c => c.Place).WithMany();
    builder.HasOne(c => c.Moderator).WithMany();
  }
}
