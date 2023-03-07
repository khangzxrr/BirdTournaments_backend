using Ardalis.ApiEndpoints;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class GetById : EndpointBaseAsync
  .WithRequest<GetByIdRequest>
  .WithActionResult<GetByIdResponse>
{

  private readonly IRepository<Competition> _competitionsRepository;

  public GetById(IRepository<Competition> competitionsRepository)
  {
    _competitionsRepository = competitionsRepository;
  }

  [HttpGet(GetByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Get a competition",
    Description = "Get a competition",
    OperationId = "Competitions.Get",
    Tags = new[] { "CompetitionEndpoints" }
    )]
  public override async Task<ActionResult<GetByIdResponse>> HandleAsync([FromRoute] GetByIdRequest request, CancellationToken cancellationToken = default)
  {
    var competition = await _competitionsRepository.GetByIdAsync(request.Id);

    if (competition == null)
    {
      return NotFound();
    }

    var competitionRecord = new CompetitionRecord(competition.Id, competition.Date, competition.Place.Address, competition.BirdType.Name, competition.Participants.First().Bird.Elo, competition.Status.Name);
    
    return Ok(new GetByIdResponse(competitionRecord));
  }
}
