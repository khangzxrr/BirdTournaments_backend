using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdTournaments.Core.BirdAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BirdTournaments.Infrastructure.Data.Config;
public class RankConfiguration : IEntityTypeConfiguration<Rank>
{
  public void Configure(EntityTypeBuilder<Rank> builder)
  {
    
    builder.Property(r => r.Name).HasMaxLength(100).IsRequired();
    builder.Property(r => r.MinElo).IsRequired();
  }
}
