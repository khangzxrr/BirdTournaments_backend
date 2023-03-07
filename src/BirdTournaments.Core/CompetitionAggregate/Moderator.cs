using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BirdTournaments.Core.UserAggregate;
using BirdTournaments.SharedKernel;
using BirdTournaments.SharedKernel.Interfaces;

namespace BirdTournaments.Core.ParticipantAggregate;
public class Moderator: EntityBase, IAggregateRoot
{
  public string Name { get; }
  public User User { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  private Moderator() { }

  public Moderator(string name)
  {
    Name = name;
  }

  public void SetUser(User user)
  {
    Guard.Against.Null(user);

    User = user;
  }



}
