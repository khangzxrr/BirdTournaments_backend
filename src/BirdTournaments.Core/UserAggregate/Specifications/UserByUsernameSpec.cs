using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace BirdTournaments.Core.UserAggregate.Specifications;
public class UserByUsernameSpec : Specification<User>, ISingleResultSpecification
{
  public UserByUsernameSpec(string username)
  {
    Query
      .Where(u => u.UserName == username);
  }
}
