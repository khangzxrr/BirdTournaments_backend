using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.SharedKernel;
using BirdTournaments.SharedKernel.Interfaces;

namespace BirdTournaments.Core.ParticipantAggregate;
public class Competition: EntityBase, IAggregateRoot
{
  public Place Place { get; private set; }
  public DateTime Date { get; private set; }
  public BirdType BirdType { get; private set; }
  public Moderator Moderator { get; private set; }

  public CompetitionStatus Status { get; private set; }

  public readonly List<Participant> participants = new List<Participant>();
  public IReadOnlyCollection<Participant> Participants => participants.AsReadOnly();

#pragma warning disable CS8618
  private Competition() { }

  public Competition(DateTime date)
  {
    Date = date;
  }


  public void SetDate(DateTime date)
  {
    Date = date;
  }
  public void AddParticipant(Participant participant)
  {
    participants.Add(participant);
  }

  public void SetPlace(Place place)
  {
    Place = Guard.Against.Null(place, nameof(place));
  }

  public void SetBirdType(BirdType birdType)
  {
    BirdType = Guard.Against.Null(birdType, nameof(birdType));
  }

  public void SetStatus(CompetitionStatus status)
  {
    Status = Guard.Against.Null(status, nameof(status));
  }

  public void SetModerator(Moderator moderator)
  {
    Moderator = Guard.Against.Null(moderator, nameof(moderator));
  }

}
