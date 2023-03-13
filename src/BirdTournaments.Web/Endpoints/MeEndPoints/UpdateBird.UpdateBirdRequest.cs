using System.ComponentModel.DataAnnotations;

namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public class UpdateBirdRequest
{
  public const string Route = "/me/birds";

  [Required]
  public int? Id { get; set; }
  public string? Name { get; set; }
  public string? Image { get; set; }
  public int? BirdTypeId { get; set; }
}
