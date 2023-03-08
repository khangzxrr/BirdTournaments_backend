using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using BirdTournaments.Core.ParticipantAggregate;

namespace BirdTournaments.Core.CompetitionAggregate.Specifications;
public class CompetitionByVerifyingStatusSpec : Specification<Competition>
{
  public CompetitionByVerifyingStatusSpec()
  {
    Query
      .Where(c => c.Status == CompetitionStatus.WaitingForVerify)
      .Include(c => c.Participants)
        .ThenInclude(p => p.Bird)
      .Include(c => c.Participants)
        .ThenInclude(p => p.BirdOwner);
  }
}
