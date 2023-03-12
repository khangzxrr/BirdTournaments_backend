using System.ComponentModel.DataAnnotations;

namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public class CreateBirdOwnerRequest
{
  public const string Route = "/me/birdowner";

  #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  [Required]

  public string name { get; set; }


  [Required]
  public string address { get; set; }

  [Required]
  public string phoneNumber { get; set; }



}
