using Ardalis.ApiEndpoints;
using BirdTournaments.Core.Interfaces;
using BirdTournaments.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class SubmitResult : EndpointBaseAsync
  .WithRequest<SubmitResultRequest>
  .WithoutResult
{
  private readonly ICurrentUserService _currentUserService;
  private readonly ICompetitionService _competitionService;

  public SubmitResult(ICurrentUserService currentUserService, ICompetitionService competitionService)
  {
    _currentUserService = currentUserService;
    _competitionService = competitionService;
  }

  [HttpPost(SubmitResultRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Submit result to a competition",
    Description = "Submit result to a competition",
    OperationId = "Competitions.SubmitResult",
    Tags = new[] { "CompetitionEndpoints" }
    )]
  public override Task<ActionResult> HandleAsync(
    SubmitResultRequest request, CancellationToken cancellationToken = default)
  {
    
  }
}
