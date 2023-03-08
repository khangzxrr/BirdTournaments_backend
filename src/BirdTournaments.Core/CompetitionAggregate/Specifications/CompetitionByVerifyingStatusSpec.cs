using Ardalis.Specification;
using BirdTournaments.Core.ParticipantAggregate;

namespace BirdTournaments.Core.CompetitionAggregate.Specifications;
public class CompetitionByVerifyingStatusSpec : Specification<Competition>
{
  public CompetitionByVerifyingStatusSpec()
  {
    Query
      .Where(c => c.Status == CompetitionStatus.WaitingForVerify)
      .Include(c => c.BirdType)
      .Include(c => c.Place)
      .Include(c => c.Participants)
        .ThenInclude(p => p.Bird)
      .Include(c => c.Participants)
        .ThenInclude(p => p.BirdOwner);
  }
}
