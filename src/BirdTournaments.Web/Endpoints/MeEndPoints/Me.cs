using Ardalis.ApiEndpoints;
using BirdTournaments.Core.BirdOwnerAggregate;
using BirdTournaments.Core.BirdOwnerAggregate.Specifications;
using BirdTournaments.SharedKernel.Interfaces;
using BirdTournaments.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public class Me : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<MeResponse>
{

  private readonly ICurrentUserService _currentUserService;
  private readonly IRepository<BirdOwner> _birdOwnerRepository;

  public Me(ICurrentUserService currentUserService, IRepository<BirdOwner> birdOwnerRepository) 
  {
    _currentUserService = currentUserService;
    _birdOwnerRepository = birdOwnerRepository;
  }


  [HttpGet("/me")]
  [Authorize]
  [SwaggerOperation(
    Summary = "get authenticated user information",
    Description = "get authenticated user information",
    OperationId = "Authen.Me",
    Tags = new[] { "Authen" }
    )
  ]
  public override async Task<ActionResult<MeResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {
    var birdOwnerId = _currentUserService.TryParseBirdOwnerId();

    var birdOwnerSpec = new BirdOwnerWithBirdsById(birdOwnerId);
    var birdOwner = await _birdOwnerRepository.FirstOrDefaultAsync(birdOwnerSpec);

    if (birdOwner == null)
    {
      return BadRequest("birdowner is null?");
    }

    var birdRecords = birdOwner.Birds.Select(BirdRecord.FromEntity);
    var birdOwnerRecord = BirdOwnerRecord.FromEntity(birdOwner);

    var response = new MeResponse(birdRecords, birdOwnerRecord);


    return Ok(response);
  }
}
