using System.Data;
using System.Runtime.InteropServices;
using Ardalis.ApiEndpoints;
using BirdTournaments.Core.Interfaces;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.Core.Services;
using BirdTournaments.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.AdminCompetitionEndpoints;

public class SetWinner : EndpointBaseAsync
  .WithRequest<SetWinnerRequest>
  .WithActionResult
{

  private readonly ICompetitionService _competitionService;
  public SetWinner(ICompetitionService competitionService)
  {
    _competitionService = competitionService;
  }


  [HttpPost(SetWinnerRequest.Route)]
  [Authorize(Roles = "admin")]
  [SwaggerOperation(
    Summary = "Set winner for competition which need to verify",
    Description = "Set winner for competition which need to verify",
    OperationId = "Admin.SetWinnerForVerifyCompetition",
    Tags = new[] { "Admin" }
    )
  ]
  public override async Task<ActionResult> HandleAsync(SetWinnerRequest request, CancellationToken cancellationToken = default)
  {
    try
    {
      var result = await _competitionService.SetWinnerCompetition(request.competitionId, request.winnerId, request.loserId);

      return Ok("Success");
    }
    catch(Exception ex)
    {
      return BadRequest(ex.Message);
    }
    


  }
}
