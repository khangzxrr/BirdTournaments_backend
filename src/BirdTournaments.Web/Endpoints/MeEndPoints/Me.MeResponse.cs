namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public class MeResponse
{
  public BaseUserRecord BaseUserRecord { get; set; }

  public MeResponse(BaseUserRecord baseUserRecord)
  {
    BaseUserRecord = baseUserRecord; 
  }
}
