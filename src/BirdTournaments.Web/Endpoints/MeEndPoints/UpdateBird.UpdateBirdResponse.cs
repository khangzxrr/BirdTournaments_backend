namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public class UpdateBirdResponse
{
  public UpdateBirdResponse(BirdRecord bird)
  {
    this.Bird = bird;
  }

  public BirdRecord Bird { get; set; }
}
