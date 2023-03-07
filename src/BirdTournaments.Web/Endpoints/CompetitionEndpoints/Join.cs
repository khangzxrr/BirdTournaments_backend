using Ardalis.ApiEndpoints;
using BirdTournaments.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class Join : EndpointBaseAsync
  .WithRequest<JoinCompetitionRequest>
  .WithoutResult
{

  private readonly ICompetitionService _competitionService;
  public Join(ICompetitionService competitionService)
  {
     _competitionService = competitionService;
  }

  [HttpPost(JoinCompetitionRequest.Route)]
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
      await _competitionService.AddOpponent(request.CompetitionId, request.BirdId, request.OwnerId);
      return Ok("success");
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
}
