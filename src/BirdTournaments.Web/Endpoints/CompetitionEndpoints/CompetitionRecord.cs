using BirdTournaments.Core.ParticipantAggregate;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public record CompetitionRecord(int Id, DateTime date, string place, string birdType, int elo, string status, string challengerName, string image, IEnumerable<ParticipantRecord> participantRecords)
{
  public static CompetitionRecord FromEntity(Competition competition)
  {
    var participant = competition.Participants.First()!;

    var participantRecords = competition.Participants.Select(ParticipantRecord.FromEntity);

    var competitionRecord = new CompetitionRecord(
      competition.Id, 
      competition.Date, 
      competition.Place.Address, 
      competition.BirdType.Name, 
      participant.Bird.Elo, 
      competition.Status.Name, 
      participant.BirdOwner.Name,
      participant.Bird.Image,
      participantRecords);




    return competitionRecord;
  }
}
