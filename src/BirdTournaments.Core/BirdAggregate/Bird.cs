using Ardalis.GuardClauses;
using BirdTournaments.Core.BirdOwnerAggregate;
using BirdTournaments.SharedKernel;
using BirdTournaments.SharedKernel.Interfaces;

namespace BirdTournaments.Core.BirdAggregate;
public class Bird : EntityBase, IAggregateRoot
{
  public string Name { get; private set; }
  public string Image { get; private set; }
  public int Elo { get;  }


  public Rank Rank { get; private set; }
  public BirdType BirdType { get; private set; }

  #pragma warning disable CS8618 // Required by Entity Framework
  private Bird() { }
  public Bird(
      string name,
      string image,
      int elo
    )
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Image = Guard.Against.NullOrEmpty(image, nameof(image));
    Elo = elo;
  }

  public void SetRank(Rank rank)
  {
    Rank = Guard.Against.Null(rank, nameof(rank));
  }

  public void SetBirdType(BirdType birdType)
  {
    BirdType = Guard.Against.Null(birdType, nameof(birdType));    
  }

  public void SetName(string name)
  {
    this.Name = Guard.Against.NullOrEmpty(name, nameof(name));
  }

  public void SetImage(string image)
  {
    this.Image = Guard.Against.NullOrEmpty(image, nameof(Name));
  }
}
