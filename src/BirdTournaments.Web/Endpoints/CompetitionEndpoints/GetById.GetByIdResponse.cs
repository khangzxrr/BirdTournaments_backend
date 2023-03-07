namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class GetByIdResponse
{
  public GetByIdResponse(CompetitionRecord competitionRecord)
  {
    this.competitionRecord = competitionRecord;
  }

  CompetitionRecord competitionRecord { get; set; } 
}
