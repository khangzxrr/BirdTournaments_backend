using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdTournaments.Core.BirdAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BirdTournaments.Infrastructure.Data.Config;
public class BirdTypeConfiguration : IEntityTypeConfiguration<BirdType>
{
  public void Configure(EntityTypeBuilder<BirdType> builder)
  {
    builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
  }
}
