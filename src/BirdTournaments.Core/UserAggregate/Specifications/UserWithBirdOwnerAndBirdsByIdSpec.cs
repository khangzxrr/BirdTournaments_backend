using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace BirdTournaments.Core.UserAggregate.Specifications;
public class UserWithBirdOwnerAndBirdsByIdSpec: Specification<User>, ISingleResultSpecification
{
  public UserWithBirdOwnerAndBirdsByIdSpec(int userId)
  {
    Query
      .Where(u => u.Id == userId)
      .Include(u => u.BirdOwner)
      .ThenInclude(bo => bo.Birds)
      .ThenInclude(b => b.Rank);
  }
}
