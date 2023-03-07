using System.ComponentModel.DataAnnotations;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class CreateCompetitionRequest
{
  public const string Route = "/Competitions";

  [Required]
  public string? Date { get; set; }
  [Required]
  public int placeId { get; set; }
  [Required]
  public int birdTypeId { get; set; }
  [Required]
  public int creatorBirdId { get; set; }
}
