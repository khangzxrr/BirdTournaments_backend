using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ardalis.ApiEndpoints;
using BirdTournaments.Core.Interfaces;
using BirdTournaments.Core.UserAggregate;
using BirdTournaments.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.AuthenticationEndpoints;

public class AuthenLogin : EndpointBaseAsync
  .WithRequest<AuthenRequest>
  .WithActionResult<AuthenResponse>
{

  private readonly IAuthenticationService _authenticationService;
  private readonly ITokenService _tokenService;
  public AuthenLogin(IAuthenticationService authenticationService, ITokenService tokenService)
  {
    _authenticationService = authenticationService;
    _tokenService = tokenService;
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
      string token = _tokenService.GenerateToken(user);

      var response = new AuthenResponse(token);

      return Ok(response);
    }
    catch(Exception)
    {
      return NotFound("Wrong username password");
    }
  }
}
