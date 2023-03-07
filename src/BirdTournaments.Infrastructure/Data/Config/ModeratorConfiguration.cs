using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdTournaments.Core.ParticipantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BirdTournaments.Infrastructure.Data.Config;
public class ModeratorConfiguration : IEntityTypeConfiguration<Moderator>
{
  public void Configure(EntityTypeBuilder<Moderator> builder)
  {
    builder.Property(m => m.Name).HasMaxLength(100).IsRequired();
    builder.HasOne(m => m.User).WithMany();
  }
}
