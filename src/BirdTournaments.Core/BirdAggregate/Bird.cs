using Ardalis.GuardClauses;
using BirdTournaments.Core.BirdOwnerAggregate;
using BirdTournaments.SharedKernel;
using BirdTournaments.SharedKernel.Interfaces;

namespace BirdTournaments.Core.BirdAggregate;
public class Bird : EntityBase, IAggregateRoot
{
  public string Name { get;  }
  public string Image { get;  }
  public int Elo { get;  }


  public Rank Rank { get; }
  public BirdType BirdType { get; }
  public BirdOwner BirdOwner { get; }

  #pragma warning disable CS8618 // Required by Entity Framework
  private Bird() { }
  public Bird(
      string name,
      string image,
      int elo,
      Rank rank,
      BirdType birdType,
      BirdOwner birdOwner
    )
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Image = Guard.Against.NullOrEmpty(image, nameof(image));
    Elo = elo;

    BirdType = birdType;
    Rank = rank;
    BirdOwner = birdOwner;
  }
}
