
using Ardalis.Specification;
using BirdTournaments.Core.ParticipantAggregate;

namespace BirdTournaments.Core.CompetitionAggregate.Specifications;
public class CompetitionByIdSpec: Specification<Competition>, ISingleResultSpecification
{
  public CompetitionByIdSpec(int id)
  {
    Query
      .Where(c => c.Id == id)
      .Include(c => c.BirdType)
      .Include(c => c.Place)
      .Include(c => c.Participants)
        .ThenInclude(p => p.BirdOwner)
      .Include(c => c.Participants)
        .ThenInclude(p => p.Bird);
      
      
    
  }
}
