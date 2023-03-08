using System.ComponentModel.DataAnnotations;

namespace BirdTournaments.Web.Endpoints.AuthenticationEndpoints;

public class RegisterRequest
{

  public const string Route = "/register";

  [Required]
  [MaxLength(100)]
  public string Username { get; set; }
  [Required]
  [MaxLength(100)]
  [MinLength(6)]
  public string Password { get; set; }
  [Required]
  [DataType(DataType.EmailAddress)]
  public string Email { get; set; }

  public RegisterRequest(string username, string password, string email)
  {
    Username = username;
    Password = password;
    Email = email;
  }
}
