using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BirdTournaments.SharedKernel;

namespace BirdTournaments.Core.ParticipantAggregate;
public class Region: EntityBase
{
  public string Name { get; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  private Region() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  
  public Region(string name)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
  }
}
