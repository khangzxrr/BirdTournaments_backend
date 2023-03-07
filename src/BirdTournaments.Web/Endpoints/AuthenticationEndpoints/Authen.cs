using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.AuthenticationEndpoints;

public class Authen : EndpointBaseAsync
  .WithRequest<AuthenRequest>
  .WithActionResult<AuthenResponse>
{

  [HttpPost("/Login")]
  [SwaggerOperation(
    Summary = "Login by username/password",
    Description = "Login by username/password",
    OperationId = "Authen.Login",
    Tags = new[] { "Authen" }
    )
  ]
  public override Task<ActionResult<AuthenResponse>> HandleAsync(AuthenRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
