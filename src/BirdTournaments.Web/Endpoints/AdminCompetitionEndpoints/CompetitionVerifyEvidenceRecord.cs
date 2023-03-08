using BirdTournaments.Core.ParticipantAggregate;

namespace BirdTournaments.Web.Endpoints.AdminCompetitionEndpoints;

public record CompetitionVerifyEvidenceRecord(int id, int ownerId, string ownerName, string evidenceUrl)
{
  public static CompetitionVerifyEvidenceRecord FromEntity(Participant participant)
  {
    return new CompetitionVerifyEvidenceRecord(participant.Id, participant.BirdOwner.Id, participant.BirdOwner.Name, participant.SubmitUrl);
  }
}
