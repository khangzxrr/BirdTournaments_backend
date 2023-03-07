namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class GetByIdResponse
{
  public GetByIdResponse(CompetitionRecord competitionRecord)
  {
    this.competitionRecord = competitionRecord;
  }

  public CompetitionRecord competitionRecord { get; set; } 
}
