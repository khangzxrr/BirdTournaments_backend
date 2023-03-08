using BirdTournaments.Core.ParticipantAggregate;

namespace BirdTournaments.Web.Endpoints.AdminCompetitionEndpoints;

public record CompetitionVerifyRecord(int Id, DateTime date, string place, string birdType, int elo, string status, string challengerName, string image, IEnumerable<CompetitionVerifyEvidenceRecord> evidences)
{
  public static CompetitionVerifyRecord FromEntity(Competition competition)
  {

    var evidences = competition.Participants.Select(CompetitionVerifyEvidenceRecord.FromEntity);

    var competitionVerifyRecord = new CompetitionVerifyRecord(
      competition.Id,
      competition.Date,
      competition.Place.Address,
      competition.BirdType.Name,
      competition.Participants.First().Bird.Elo,
      competition.Status.Name,
      competition.Participants.First().BirdOwner.Name,
      competition.Participants.First().Bird.Image,
      evidences);

    return competitionVerifyRecord;
  }
}
