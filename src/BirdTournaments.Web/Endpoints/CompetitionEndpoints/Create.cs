
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

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateCompetitionRequest>
  .WithActionResult<CompetitionRecord>
{
  private readonly ICompetitionService _competitionService;
  private readonly IRepository<BirdOwner> _birdOwnerRepository;

  public Create(ICompetitionService competitionService, IRepository<BirdOwner> birdOwnerRepository)
  {
    _competitionService = competitionService;
    _birdOwnerRepository = birdOwnerRepository;
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
      var identity = HttpContext.User.Identity as ClaimsIdentity;

      var birdOwnerIdClaim = identity!.Claims.FirstOrDefault(x => x.Type == "birdOwnerId");
      Guard.Against.Null(birdOwnerIdClaim, nameof(birdOwnerIdClaim));

      var birdOwnerId = int.Parse(birdOwnerIdClaim.Value!);
      //======================



      var competition = await _competitionService.AddNewCompetition(request.placeId, request.birdTypeId, date, request.creatorBirdId, birdOwnerId);

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
