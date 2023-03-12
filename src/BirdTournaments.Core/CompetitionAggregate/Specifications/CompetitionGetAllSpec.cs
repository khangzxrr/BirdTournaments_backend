using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using BirdTournaments.Core.ParticipantAggregate;

namespace BirdTournaments.Core.CompetitionAggregate.Specifications;
public class CompetitionGetAllSpec : Specification<Competition>
{
    public CompetitionGetAllSpec()
  {
    Query
      .Include(c => c.BirdType)
      .Include(c => c.Place)
      .Include(c => c.Participants)
        .ThenInclude(p => p.BirdOwner)
      .Include(c => c.Participants)
        .ThenInclude(p => p.Bird);

  }

}
