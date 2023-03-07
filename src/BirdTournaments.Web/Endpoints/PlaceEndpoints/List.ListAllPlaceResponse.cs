namespace BirdTournaments.Web.Endpoints.PlaceEndpoints;

public class ListAllPlaceResponse
{
  public IEnumerable<PlaceRecord> PlaceRecords { get; set; }

  public ListAllPlaceResponse(IEnumerable<PlaceRecord> placeRecords)
  {
    PlaceRecords = placeRecords;
  }
}
