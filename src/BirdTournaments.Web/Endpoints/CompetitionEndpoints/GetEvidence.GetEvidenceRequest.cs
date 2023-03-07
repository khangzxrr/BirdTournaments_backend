namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class GetEvidenceRequest
{
  public const string Route = "/Competitions/{CompetitionId:int}/evidence";

  public static string BuildRoute(int CompetitionId) => Route.Replace("{CompetitionId:int}", CompetitionId.ToString());

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public int CompetitionId { get; set; }
}
