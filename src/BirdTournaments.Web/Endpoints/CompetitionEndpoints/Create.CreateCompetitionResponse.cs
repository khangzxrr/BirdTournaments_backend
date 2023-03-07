namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class CreateCompetitionResponse
{
  public CreateCompetitionResponse(CompetitionRecord competitionRecord)
  {
    CompetitionRecord = competitionRecord;
  }
  public CompetitionRecord CompetitionRecord { get; set; }
}
