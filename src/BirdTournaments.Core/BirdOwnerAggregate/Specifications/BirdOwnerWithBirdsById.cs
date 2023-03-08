using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace BirdTournaments.Core.BirdOwnerAggregate.Specifications;
public class BirdOwnerWithBirdsById : Specification<BirdOwner>, ISingleResultSpecification
{
  public BirdOwnerWithBirdsById(int ownerId)
  {
    Query
      .Where(bo => bo.Id == ownerId)
      .Include(bo => bo.Birds)
      .ThenInclude(b => b.Rank);
  }
}
