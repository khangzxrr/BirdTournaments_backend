namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class GetEvidenceResponse
{
  public IEnumerable<EvidenceRecord> EvidenceRecords { get; set; }
  public GetEvidenceResponse(IEnumerable<EvidenceRecord> evidenceRecords)
  {
    EvidenceRecords = evidenceRecords;
  }
}
