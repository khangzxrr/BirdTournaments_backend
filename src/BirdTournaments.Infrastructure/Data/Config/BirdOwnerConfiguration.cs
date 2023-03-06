using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdTournaments.Core.BirdOwnerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BirdTournaments.Infrastructure.Data.Config;
public class BirdOwnerConfiguration : IEntityTypeConfiguration<BirdOwner>
{
  public void Configure(EntityTypeBuilder<BirdOwner> builder)
  {
    builder.Property(b => b.Address).HasMaxLength(200).IsRequired();
    builder.Property(b => b.PhoneNumber).HasMaxLength(12).IsRequired();
    builder.Property(b => b.Name).HasMaxLength(100).IsRequired();

    builder.HasOne(b => b.User).WithOne(u => u.BirdOwner).HasForeignKey<BirdOwner>(bo => bo.UserId);

    builder.Metadata.FindNavigation(nameof(BirdOwner.Birds))?.SetPropertyAccessMode(PropertyAccessMode.Field);
  }
}
