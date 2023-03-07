
using BirdTournaments.SharedKernel.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using BirdTournaments.Core.UserAggregate;
using BirdTournaments.Core.BirdOwnerAggregate;
using System.Security.Claims;
using Ardalis.GuardClauses;
using BirdTournaments.Web.Interfaces;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateCompetitionRequest>
  .WithActionResult<CompetitionRecord>
{
  private readonly ICompetitionService _competitionService;
  private readonly ICurrentUserService _currentUserService;

  public Create(ICompetitionService competitionService, ICurrentUserService currentUserService)
  {
    _competitionService = competitionService;
    _currentUserService = currentUserService;
  }

  

  [HttpPost("/Competitions")]
  [Authorize]
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

      //=========== identity 
      Guard.Against.Null(_currentUserService.BirdOwnerId);
      var birdOwnerId = int.Parse(_currentUserService.BirdOwnerId);
      //======================

      var competition = await _competitionService.AddNewCompetition(request.placeId, request.birdTypeId, date, request.creatorBirdId, birdOwnerId);

      var response = new CreateCompetitionResponse(CompetitionRecord.FromEntity(competition));

      return Ok(response);

    }
    catch(Exception ex)
    {
      return BadRequest(ex.Message);
    }

    

    
  }
}
