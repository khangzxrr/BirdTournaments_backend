using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.Core.BirdOwnerAggregate;
using BirdTournaments.SharedKernel;
using BirdTournaments.SharedKernel.Interfaces;

namespace BirdTournaments.Core.ParticipantAggregate;
public class Participant : EntityBase
{
  public Bird Bird { get; private set; }
  public BirdOwner BirdOwner { get; private set; }
  public int EloGain { get; private set; }
  public string SubmitUrl { get; private set; }
  public ParticipantStatus Status { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public Participant() {
    EloGain = 10;
    SubmitUrl = "";
  }


  public Participant(Bird bird, BirdOwner birdOwner, ParticipantStatus status)
  {
    Bird = bird;
    BirdOwner = birdOwner;
    Status = status;
  }

  public void SetBird(Bird bird)
  {
    Guard.Against.Null(bird, nameof(bird));
    this.Bird = bird;
  }

  public void SetSubmitUrl(string submitUrl)
  {
    Guard.Against.Null(submitUrl, nameof(submitUrl));

    this.SubmitUrl = submitUrl;
}

  public void SetBirdOwner(BirdOwner birdOwner)
  {
    Guard.Against.Null(birdOwner, nameof(birdOwner));
    this.BirdOwner = birdOwner;
  }

  public void SetParticipantStatus(ParticipantStatus status)
  {
    Guard.Against.Null(status, nameof(status));
    this.Status = status;
  }

}
