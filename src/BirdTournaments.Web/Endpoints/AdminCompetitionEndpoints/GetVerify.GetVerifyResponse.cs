
namespace BirdTournaments.Web.Endpoints.AdminCompetitionEndpoints;

public class GetVerifyResponse
{
  public IEnumerable<CompetitionVerifyRecord> competitionRecords { get; set; }
  public GetVerifyResponse(IEnumerable<CompetitionVerifyRecord> competitionRecords)
  {
    this.competitionRecords = competitionRecords;
  } 
}
