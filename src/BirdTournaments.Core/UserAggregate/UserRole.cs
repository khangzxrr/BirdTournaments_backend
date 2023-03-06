using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace BirdTournaments.Core.UserAggregate;
public class UserRole : SmartEnum<UserRole>
{
  public static readonly UserRole admin = new(nameof(admin), 1);
  public static readonly UserRole player = new(nameof(player), 0);

  protected UserRole(string name, int value) : base(name, value) { }
}
