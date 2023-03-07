using Ardalis.ApiEndpoints;
using BirdTournaments.Core.CompetitionAggregate.Specifications;
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
    var competitionSpec = new CompetitionByIdSpec(request.Id);
    var competition = await _competitionsRepository.FirstOrDefaultAsync(competitionSpec);

    if (competition == null)
    {
      return NotFound();
    }

    var competitionRecord = new CompetitionRecord(
      competition.Id, 
      competition.Date, 
      competition.Place.Address, 
      competition.BirdType.Name, 
      competition.Participants.First().Bird.Elo, 
      competition.Status.Name,
      competition.Participants.First().BirdOwner.Name);
    
    return Ok(new GetByIdResponse(competitionRecord));
  }
}
