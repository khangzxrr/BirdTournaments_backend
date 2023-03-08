using BirdTournaments.Core.BirdAggregate;

namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public record BirdRecord(int id, string name, int elo, string rankName)
{
  public static BirdRecord FromEntity(Bird bird)
  {
    return new BirdRecord(bird.Id, bird.Name, bird.Elo, bird.Rank.Name);
  }
}
