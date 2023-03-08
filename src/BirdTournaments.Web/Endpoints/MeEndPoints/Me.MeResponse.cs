namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public class MeResponse
{
  public IEnumerable<BirdRecord> BirdRecords { get; set; }
  public BirdOwnerRecord BirdOwnerRecord { get; set; }

  public MeResponse(IEnumerable<BirdRecord> records, BirdOwnerRecord birdOwnerRecord)
  {
    BirdRecords = records;
    BirdOwnerRecord = birdOwnerRecord;
  }
}
