using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ardalis.ApiEndpoints;
using BirdTournaments.Core.Interfaces;
using BirdTournaments.Core.UserAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.AuthenticationEndpoints;

public class Authen : EndpointBaseAsync
  .WithRequest<AuthenRequest>
  .WithActionResult<AuthenResponse>
{

  private readonly IAuthenticationService _authenticationService;
  private readonly IConfiguration _configuration;
  public Authen(IAuthenticationService authenticationService, IConfiguration configuration)
  {
    _authenticationService = authenticationService;
    _configuration = configuration;
  }

  private string GenerateToken(User user)
  {
    string key = _configuration["Jwt:Key"]!;
    string issuer = _configuration["Jwt:Issuer"]!;
    string audience = _configuration["Jwt:Audience"]!;

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
      new Claim(ClaimTypes.NameIdentifier,user.UserName),
      new Claim(ClaimTypes.Role, user.Role.Name),
      new Claim("birdOwnerId", (user.BirdOwner == null) ? "" : user.BirdOwner.Id.ToString())
    };

     var token = new JwtSecurityToken(
      issuer, 
      audience, 
      claims, 
      expires: DateTime.Now.AddMinutes(90),
      signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);

  }

  [HttpPost("/Login")]
  [SwaggerOperation(
    Summary = "Login by username/password",
    Description = "Login by username/password",
    OperationId = "Authen.Login",
    Tags = new[] { "Authen" }
    )
  ]
  public override async Task<ActionResult<AuthenResponse>> HandleAsync(AuthenRequest request, CancellationToken cancellationToken = default)
  {
    try
    {
      var user = await _authenticationService.AuthenticationAsync(request.Username, request.Password);
      string token = GenerateToken(user);

      var response = new AuthenResponse(token);

      return Ok(response);
    }
    catch(Exception)
    {
      return NotFound("Wrong username password");
    }
  }
}
