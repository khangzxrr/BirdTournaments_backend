using Ardalis.GuardClauses;
using BirdTournaments.SharedKernel;

namespace BirdTournaments.Core.BirdAggregate;
public class Rank: EntityBase
{
  public string Name { get; private set; }
  public int MinElo { get; private set; }

  public Rank(string name, int minElo)
  {
    Name = name;
    MinElo = minElo;
  }
}
