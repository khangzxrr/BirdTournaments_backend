using Ardalis.ApiEndpoints;
using BirdTournaments.Core.CompetitionAggregate.Specifications;
using BirdTournaments.Core.Interfaces;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.AdminCompetitionEndpoints;

public class GetVerify : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult
{

  private readonly IRepository<Competition> _competitionRepository;

  public GetVerify( IRepository<Competition> competitionRepository)
  {
     _competitionRepository = competitionRepository;
  }

  [HttpGet(GetVerifyRequest.Route)]
  [Authorize(Roles = "admin")]
  [SwaggerOperation(
    Summary = "Get competition need to verify",
    Description = "Get competition need to verify",
    OperationId = "Admin.GetVerifyingCompetition",
    Tags = new[] { "Admin" }
    )
  ]
  public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
  {
    var competitionsSpec = new CompetitionByVerifyingStatusSpec();
    var competitions = await _competitionRepository.ListAsync(competitionsSpec);

    if (competitions == null)
    {
      return BadRequest("Something is wrong...");
    }

    var competitionVerifyRecords = competitions.Select(CompetitionVerifyRecord.FromEntity);

    var response = new GetVerifyResponse(competitionVerifyRecords);

    return Ok(response);
  }
}
