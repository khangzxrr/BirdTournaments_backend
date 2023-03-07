using System.ComponentModel.DataAnnotations;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class SubmitEvidenceRequest
{
 
  public const string Route = "/Competitions/submit_evidence";

  [Required]
  public int CompetitionId { get; set; }

  [Required]
  [MinLength(1)]
  public string? EvidenceVideoUrl { get; set; }

}
