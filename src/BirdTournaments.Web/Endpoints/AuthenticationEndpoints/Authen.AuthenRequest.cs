using System.ComponentModel.DataAnnotations;

namespace BirdTournaments.Web.Endpoints.AuthenticationEndpoints;

public class AuthenRequest
{
  [Required]
  public string Username { get; set; }
  [Required] 
  public string Password { get; set; }

  public AuthenRequest(string username, string password)
  {
    Username = username;
    Password = password;
  }
}
