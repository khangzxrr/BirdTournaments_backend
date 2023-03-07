namespace BirdTournaments.Web.Endpoints.BirdTypeEndpoints;

public class ListAllBirdResponse
{
  public IEnumerable<BirdTypeRecord> BirdTypeRecords { get; set; }

  public ListAllBirdResponse(IEnumerable<BirdTypeRecord> birdTypeRecords)
  {
    BirdTypeRecords = birdTypeRecords;
  }
}
