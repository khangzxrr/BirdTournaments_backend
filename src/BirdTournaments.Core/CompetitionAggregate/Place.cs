using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdTournaments.SharedKernel;
using BirdTournaments.SharedKernel.Interfaces;

namespace BirdTournaments.Core.ParticipantAggregate;
public class Place: EntityBase, IAggregateRoot
{
  public string Address { get; }
  public Region Region { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  private Place() { }
  public Place(string address)
  {
    Address = address;
  }

  public void SetRegion(Region region)
  {
    Region = region;
  }


}
