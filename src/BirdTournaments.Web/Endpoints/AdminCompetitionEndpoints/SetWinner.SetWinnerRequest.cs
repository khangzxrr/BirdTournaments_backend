

using System.ComponentModel.DataAnnotations;

namespace BirdTournaments.Web.Endpoints.AdminCompetitionEndpoints;

public class SetWinnerRequest
{
  public const string Route = "/admin/competitions/set_winner";

  [Required]
  public int competitionId { get; set; }
  [Required]
  public int winnerId { get; set; }
  [Required]
  public int loserId { get; set; }
}
