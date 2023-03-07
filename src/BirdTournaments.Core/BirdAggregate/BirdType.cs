using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdTournaments.SharedKernel;
using BirdTournaments.SharedKernel.Interfaces;

namespace BirdTournaments.Core.BirdAggregate;
public class BirdType: EntityBase, IAggregateRoot
{
  public string Name { get; private set; }

  public BirdType(string name)
  {
    Name = name;
  }
}
