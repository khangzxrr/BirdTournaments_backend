using Ardalis.ApiEndpoints;
using BirdTournaments.Core.CompetitionAggregate.Specifications;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class GetAll : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<GetAllResponse>
{

  private readonly IRepository<Competition> _competitionRepository;

  public GetAll(IRepository<Competition> competitionRepository)
  {
    _competitionRepository = competitionRepository;
  }

  [HttpGet("/competitions")]
  [SwaggerOperation(
    Summary = "Get all competition",
    Description = "Get all competition",
    OperationId = "Competitions.GetAll",
    Tags = new[] { "CompetitionEndpoints" }
    )]
  public override async Task<ActionResult<GetAllResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {
    var spec = new CompetitionGetAllSpec();
    var competitions = await _competitionRepository.ListAsync(spec);

    var competitionRecords = competitions.Select(CompetitionRecord.FromEntity);

    var response = new GetAllResponse(competitionRecords);

    return Ok(response);
  }
}
