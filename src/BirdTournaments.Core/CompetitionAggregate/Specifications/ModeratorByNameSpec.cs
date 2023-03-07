using Ardalis.Specification;
using BirdTournaments.Core.ParticipantAggregate;

namespace BirdTournaments.Core.CompetitionAggregate.Specifications;
public class ModeratorByNameSpec: Specification<Moderator>
{
  public ModeratorByNameSpec(string name)
  {
    Query
      .Where(moderator => moderator.Name == name);
  }
}
