using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace BirdTournaments.Core.BirdAggregate.Specifications;
public class BirdByIdAndOwnerId : Specification<Bird>, ISingleResultSpecification
{
  public BirdByIdAndOwnerId(int birdId, int ownerId)
  {
    
  }
}
