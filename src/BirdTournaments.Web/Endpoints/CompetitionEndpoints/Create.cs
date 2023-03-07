﻿
using BirdTournaments.SharedKernel.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.Core.Interfaces;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateCompetitionRequest>
  .WithActionResult<CompetitionRecord>
{
  private readonly IRepository<Competition> _repository;
  private readonly ICompetitionService _competitionService;

  public Create(IRepository<Competition> repository, ICompetitionService competitionService)
  {
    _repository = repository;
    _competitionService = competitionService;
  }

  

  [HttpPost("/Competitions")]
  [SwaggerOperation(
    Summary = "Create a new Competition",
    Description = "Create a new Competition",
    OperationId = "Competition.Create",
    Tags = new[] { "CompetitionEndpoints" }
    )
   ]
  public override async Task<ActionResult<CompetitionRecord>> HandleAsync(
    CreateCompetitionRequest request, 
    CancellationToken cancellationToken = new())
  {
    try
    {
      var date = DateTime.Parse(request.Date!);

      var competition = await _competitionService.AddNewCompetition(request.placeId, request.birdTypeId, date, request.creatorBirdId, request.creatorId);

      var response = new CreateCompetitionResponse(
        new CompetitionRecord(competition.Id, competition.Date, competition.Place.Address, competition.BirdType.Name, competition.Participants.First().Bird.Elo));
      return Ok(response);

    }
    catch(Exception ex)
    {
      return BadRequest(ex.Message);
    }

    

    
  }
}
