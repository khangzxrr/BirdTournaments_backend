using BirdTournaments.Core.ParticipantAggregate;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public record EvidenceRecord(int ownerId, string ownerName, string evidenceUrl)
{
  public static EvidenceRecord FromEntity(Participant participant)
  {
    return new EvidenceRecord(participant.BirdOwner.Id, participant.BirdOwner.Name, participant.SubmitUrl);
  }
}
