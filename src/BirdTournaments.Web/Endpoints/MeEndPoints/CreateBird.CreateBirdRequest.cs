using System.ComponentModel.DataAnnotations;

namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public class CreateBirdRequest
{
  public const string Route = "/me/birds";
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  [Required]

  public string Name { get; set; }

  [Required]
  public string Image { get; set; }
  [Required]
  public int BirdTypeId { get; set; }

}
