using System.ComponentModel.DataAnnotations;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class JoinCompetitionRequest
{
  public const string Route = "/Competitions/join";
  [Required]  
  public int CompetitionId { get; set; }
  [Required]
  public int OwnerId { get; set; }
  [Required]
  public int BirdId { get; set; }

}
