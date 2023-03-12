namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class GetAllResponse
{
  public IEnumerable<CompetitionRecord> CompetitionRecords { get; set; }

  public GetAllResponse(IEnumerable<CompetitionRecord> CompetitionRecords)
  {
    this.CompetitionRecords = CompetitionRecords;
  }
}
