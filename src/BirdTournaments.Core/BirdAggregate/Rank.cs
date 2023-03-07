using Ardalis.GuardClauses;
using BirdTournaments.SharedKernel;
using BirdTournaments.SharedKernel.Interfaces;

namespace BirdTournaments.Core.BirdAggregate;
public class Rank: EntityBase, IAggregateRoot
{
  public string Name { get; set; }
  public int MinElo { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public Rank() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

  public Rank(string name, int minElo)
  {
    Name = name;
    MinElo = minElo;
  }
}
