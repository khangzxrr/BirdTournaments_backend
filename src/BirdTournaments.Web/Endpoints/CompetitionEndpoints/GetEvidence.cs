
using Ardalis.ApiEndpoints;
using BirdTournaments.Core.CompetitionAggregate.Specifications;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class GetEvidence : EndpointBaseAsync
  .WithRequest<GetEvidenceRequest>
  .WithActionResult<GetEvidenceResponse>
{

  private readonly IRepository<Competition> _repository;

  public GetEvidence(IRepository<Competition> repository)
  {
    _repository = repository;
  }

  [HttpGet(GetEvidenceRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Get evidences from a competition",
    Description = "Get evidences from a competition",
    OperationId = "Competitions.GetEvidence",
    Tags = new[] { "CompetitionEndpoints" }
    )]
  public override async Task<ActionResult<GetEvidenceResponse>> HandleAsync([FromRoute] GetEvidenceRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new CompetitionByIdSpec(request.CompetitionId);
    var competition = await _repository.FirstOrDefaultAsync(spec);

    if (competition == null)
    {
      return NotFound();
    }

    var evidences = competition.Participants.Select(EvidenceRecord.FromEntity);

    var response = new GetEvidenceResponse(evidences);

    return Ok(response);
  }
}
