using BirdTournaments.SharedKernel;

namespace BirdTournaments.Core.CompetitionAggregate.Events;
public class CompetitionAddedOpponentEvent : DomainEventBase
{
  public int CompetitionId { get; set; }

  public CompetitionAddedOpponentEvent(int competitionId)
  {
    CompetitionId = competitionId;
  }
}
