using Ardalis.GuardClauses;
using BirdTournaments.SharedKernel;
using BirdTournaments.SharedKernel.Interfaces;

namespace BirdTournaments.Core.BirdAggregate;
public class Bird : EntityBase, IAggregateRoot
{
  public string Name { get;  }
  public string Image { get;  }
  public int Elo { get;  }
  
  public int OwnerId { get; }
  
  public Rank Rank { get; private set; }
  public BirdType BirdType { get; private set; }

  #pragma warning disable CS8618 // Required by Entity Framework
  private Bird() { }
  public Bird(
      string name,
      string image,
      int elo,
      int ownerId,
      Rank rank,
      BirdType birdType
    )
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Image = Guard.Against.NullOrEmpty(image, nameof(image));
    Elo = elo;
    OwnerId = ownerId;

    BirdType = birdType;
    Rank = rank;
  }
}
