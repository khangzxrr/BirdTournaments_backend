using System.ComponentModel.DataAnnotations;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class SubmitResultRequest
{
  public const string Route = "/Competitions/submit_result";
  [Required]
  public int CompetitionId { get; set; }
  [Required]
  public bool IsWin { get; set; }
  //[RegularExpression(@"(http|ftp|https):\/\/([\w_-]+(?:(?:\.[\w_-]+)+))([\w.,@?^=%&:\/~+#-]*[\w@?^=%&\/~+#-])")]
  //public string? EvidenceVideoUrl { get; set; }
}
