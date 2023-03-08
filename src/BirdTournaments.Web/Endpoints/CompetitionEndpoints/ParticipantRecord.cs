using BirdTournaments.Core.ParticipantAggregate;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public record ParticipantRecord(int ownerId, string ownerName, bool isWin)
{
  public static ParticipantRecord FromEntity(Participant participant)
  {
    return new ParticipantRecord(participant.BirdOwner.Id, participant.BirdOwner.Name, participant.Status.Name == ParticipantStatus.Win.Name);
  }
}
