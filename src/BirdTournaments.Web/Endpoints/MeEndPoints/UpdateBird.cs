using Ardalis.ApiEndpoints;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.Core.BirdOwnerAggregate;
using BirdTournaments.Core.BirdOwnerAggregate.Specifications;
using BirdTournaments.SharedKernel.Interfaces;
using BirdTournaments.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public class UpdateBird : EndpointBaseAsync
  .WithRequest<UpdateBirdRequest>
  .WithActionResult<UpdateBirdResponse>
{

  private readonly ICurrentUserService _currentUserService;
  private readonly IRepository<Bird> _birdRepository;
  private readonly IRepository<BirdOwner> _birdOwnerRepository;
  private readonly IRepository<BirdType> _birdTypeRepository;

  public UpdateBird(
    ICurrentUserService currentUserService, 
    IRepository<Bird> birdRepository, 
    IRepository<BirdOwner> birdOwnerRepository,
    IRepository<BirdType> birdTypeRepository)
  {
    _currentUserService = currentUserService;
    _birdRepository = birdRepository;
    _birdOwnerRepository = birdOwnerRepository;
    _birdTypeRepository = birdTypeRepository;
  }

  [HttpPut(UpdateBirdRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "update a bird of authorized user",
    Description = "update a bird of authorized user",
    OperationId = "BirdOwner.BirdUpdate",
    Tags = new[] { "BirdOwner" }
    )
  ]
  public override async Task<ActionResult<UpdateBirdResponse>> HandleAsync(UpdateBirdRequest request, CancellationToken cancellationToken = default)
  {
    if (_currentUserService.BirdOwnerId == null)
    {
      return BadRequest("Bird Owner information is not found");
    }

    var birdOwnerSpec = new BirdOwnerWithBirdsById(_currentUserService.TryParseBirdOwnerId());
    var birdOwner = await _birdOwnerRepository.FirstOrDefaultAsync(birdOwnerSpec);
    if (birdOwner == null)
    {
      return BadRequest("BirdOwner is null, maybe it shoudnt? please contact developer");
    }

    var bird = birdOwner.Birds.Where(b => b.Id == request.Id!.Value).FirstOrDefault();
    if (bird == null)
    {
      return BadRequest("Bird is null, please verify correct Bird ID or that bird is not your");
    }

    
      

    
    if (request.Image != null)
    {
      bird.SetImage(request.Image);
    }

    if (request.Name != null)
    {
      bird.SetName(request.Name);
    }

    if (request.BirdTypeId != null)
    {
      var birdType = await _birdTypeRepository.GetByIdAsync(request.BirdTypeId!.Value);
      if (birdType == null)
      {
        return BadRequest("BirdType is not found");
      }
      bird.SetBirdType(birdType);
    }

    await _birdRepository.UpdateAsync(bird);

    var response = new UpdateBirdResponse(BirdRecord.FromEntity(bird));

    return Ok(response);
  }
}
