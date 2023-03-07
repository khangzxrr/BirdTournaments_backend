namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class GetCompetitionsByRankResponse
{
  public GetCompetitionsByRankResponse(List<CompetitionRecord> competitionRecords)
  {
    CompetitionRecords = competitionRecords;
  }

  public List<CompetitionRecord> CompetitionRecords { get; set; } = new();
}
