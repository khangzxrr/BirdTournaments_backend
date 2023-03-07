namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class GetCompetitionsByRankRequest
{
  public const string Route = "/Competitions/by_rank/{rankName}";
  public static string BuildRoute(string rankName) => Route.Replace("{rankName}", rankName);

  #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public string rankName { get; set; }

}
