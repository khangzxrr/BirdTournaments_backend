using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.Core.ParticipantAggregate;

namespace BirdTournaments.Core.CompetitionAggregate.Specifications;
public class CompetitionByRank: Specification<Competition>
{
  public CompetitionByRank(Rank? rank)
  {
    Query
      .Where(c => c.Status == CompetitionStatus.WaitingForOpponent)
      //.Where(c => c.participants.First().Bird.Rank == rank)
      .Include(c => c.BirdType)
      .Include(c => c.Place)
      .Include(c => c.Participants)
      .ThenInclude(p => p.Bird);
      

  }
}
