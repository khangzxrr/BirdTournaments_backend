using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace BirdTournaments.Core.UserAggregate.Specifications;
public class UserByEmailSpec: Specification<User>, ISingleResultSpecification
{
  public UserByEmailSpec(string email)
  {
    Query
      .Where(u => u.Email == email);
  }
}
