using BirdTournaments.Core.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BirdTournaments.Infrastructure.Data.Config;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.Property(u => u.UserName)
       .HasMaxLength(100)
       .IsRequired();

    builder.Property(u => u.Email)
       .HasMaxLength(100)
       .IsRequired();

    builder.Property(u => u.Salt)
       .HasMaxLength(100)
       .IsRequired();

    builder.Property(u => u.Hash)
       .HasMaxLength(100)
       .IsRequired();

    builder.Property(u => u.Verify)
        .HasConversion(
          v => v.Value,
          v => UserVerify.FromValue(v));

    builder.Property(u => u.Role)
       .HasConversion(
          r => r.Value,
          r => UserRole.FromValue(r));
        
  }
}
