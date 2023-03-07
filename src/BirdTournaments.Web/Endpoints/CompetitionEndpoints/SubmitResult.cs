using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
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
  public override async Task<ActionResult> HandleAsync(
    SubmitResultRequest request, CancellationToken cancellationToken = default)
  {
    try
    {
      int birdOwnerId = _currentUserService.TryParseBirdOwnerId();

      var result = await _competitionService.SubmitCompetitionResult(request.CompetitionId, birdOwnerId, request.IsWin);

      return Ok("submit successfully");

    }catch(Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
}
