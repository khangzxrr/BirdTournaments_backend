using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace BirdTournaments.Core.BirdAggregate.Specifications;
public class RankByName : Specification<Rank>, ISingleResultSpecification
{
  public RankByName(string name)
  {
    Query
      .Where(r => r.Name == name);
  }
}
