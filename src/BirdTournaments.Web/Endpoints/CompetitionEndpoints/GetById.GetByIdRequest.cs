using Microsoft.Build.Framework;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class GetByIdRequest
{
  public const string Route = "/Competitions/{Id:int}";
  public static string BuildRoute(int Id) => Route.Replace("{Id:int}", Id.ToString());

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public int Id{ get; set; }

}
