using Ardalis.ApiEndpoints;
using Ardalis.Result;
using BirdTournaments.Core.Interfaces;
using BirdTournaments.Core.Services;
using BirdTournaments.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class SubmitEvidence : EndpointBaseAsync
  .WithRequest<SubmitEvidenceRequest>
  .WithoutResult
{

  private readonly ICompetitionService _competitionService;
  private readonly ICurrentUserService _currentUserService;

  public SubmitEvidence(ICompetitionService competitionService, ICurrentUserService currentUserService)
  {
    _competitionService = competitionService;
     _currentUserService = currentUserService;
  }

  [HttpPost(SubmitEvidenceRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Submit evidence to a competition",
    Description = "Submit evidence to a competition",
    OperationId = "Competitions.SubmitEvidence",
    Tags = new[] { "CompetitionEndpoints" }
    )]
  public override async Task<ActionResult> HandleAsync(SubmitEvidenceRequest request, CancellationToken cancellationToken = default)
  {
    try
    {
      var ownerId = _currentUserService.TryParseBirdOwnerId();

      await _competitionService.SubmitCompetitionEvidence(request.CompetitionId, ownerId, request.EvidenceVideoUrl!);

      return Ok("success submit evidence");
    }
    catch(Exception ex)
    {
      return BadRequest(ex.Message);
    }

  }
}
