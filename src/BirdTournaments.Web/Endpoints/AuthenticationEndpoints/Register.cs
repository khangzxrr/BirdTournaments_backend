using Ardalis.ApiEndpoints;
using BirdTournaments.Core.Interfaces;
using BirdTournaments.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.AuthenticationEndpoints;

public class AuthenRegister : EndpointBaseAsync
  .WithRequest<RegisterRequest>
  .WithActionResult<RegisterResponse>
{

  private readonly IAuthenticationService _authenticationService;
  private readonly ITokenService _tokenService;

  public AuthenRegister(IAuthenticationService authenticationService, ITokenService tokenService)
  {
     _authenticationService = authenticationService;
    _tokenService = tokenService;
  }


  [HttpPost(RegisterRequest.Route)]
  [SwaggerOperation(
    Summary = "Register and get token",
    Description = "Register and get token",
    OperationId = "Authen.Register",
    Tags = new[] { "Authen" }
    )
  ]

  public override async Task<ActionResult<RegisterResponse>> HandleAsync(
    RegisterRequest request, CancellationToken cancellationToken = default)
  {
    var userResult = await _authenticationService.CreateNewUserAsync(request.Username, request.Password, request.Email);

    if (userResult == null)
    {
      return BadRequest("server error");
    }

    if (!userResult.IsSuccess)
    {
      return BadRequest(userResult.Errors);
    }

    var token = _tokenService.GenerateToken(userResult.Value);
    var response = new RegisterResponse(token);

    return Ok(response);
  }
}
