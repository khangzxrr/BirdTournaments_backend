using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using BirdTournaments.Core.Interfaces;
using BirdTournaments.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class Join : EndpointBaseAsync
  .WithRequest<JoinCompetitionRequest>
  .WithoutResult
{

  private readonly ICompetitionService _competitionService;
  private readonly ICurrentUserService _currentUserService;
  public Join(ICompetitionService competitionService, ICurrentUserService currentUserService)
  {
     _competitionService = competitionService;
    _currentUserService = currentUserService;
  }

  
  [HttpPost(JoinCompetitionRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Join a competition",
    Description = "Join a competition",
    OperationId = "Competitions.Join",
    Tags = new[] { "CompetitionEndpoints" }
    )]
  public override async Task<ActionResult> HandleAsync(JoinCompetitionRequest request, CancellationToken cancellationToken = default)
  {
    try
    {
      Guard.Against.Null(_currentUserService.BirdOwnerId);

      int birdOwnerId = int.Parse(_currentUserService.BirdOwnerId);

      await _competitionService.AddOpponent(request.CompetitionId, request.BirdId, birdOwnerId);
      return Ok("success");
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
}
