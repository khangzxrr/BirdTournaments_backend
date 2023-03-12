using Ardalis.ApiEndpoints;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.Core.BirdAggregate.Specifications;
using BirdTournaments.Core.BirdOwnerAggregate;
using BirdTournaments.Core.BirdOwnerAggregate.Specifications;
using BirdTournaments.SharedKernel.Interfaces;
using BirdTournaments.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public class CreateBird : EndpointBaseAsync
  .WithRequest<CreateBirdRequest>
  .WithActionResult<BirdRecord>

{

  private readonly IRepository<Bird> _birdRepository;
  private readonly IRepository<BirdOwner> _birdOwnerRepository;
  private readonly IRepository<BirdType> _birdTypeRepository;
  private readonly IRepository<Rank> _birdRankRepository;
  private readonly ICurrentUserService _currentUserService;
  public CreateBird(IRepository<Bird> birdRepository, 
    IRepository<BirdType> birdTypeRepository, 
    IRepository<Rank> rankRepository,
    IRepository<BirdOwner> birdOwnerRepository,
    ICurrentUserService currentUserService
    )
  {
    _birdRepository = birdRepository;
    _birdTypeRepository = birdTypeRepository;
    _birdRankRepository = rankRepository;
    _currentUserService = currentUserService; 
    _birdOwnerRepository = birdOwnerRepository;
  }

  [HttpPost(CreateBirdRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Create a new bird",
    Description = "Create a new bird",
    OperationId = "BirdOwner.CreateBird",
    Tags = new[] { "BirdOwner" }
    )
   ]
  public override async Task<ActionResult<BirdRecord>> HandleAsync(CreateBirdRequest request, CancellationToken cancellationToken = default)
  {
    if (_currentUserService.BirdOwnerId.IsNullOrEmpty())
    {
      return BadRequest("bird owner info is not found! Suggest: create new or refresh token by /login");
    }

    var birdType = await _birdTypeRepository.GetByIdAsync(request.BirdTypeId);
    if (birdType == null) {
      return BadRequest(birdType);
    }

    var rankSpec = new RankByName("silver");
    var rank = await _birdRankRepository.FirstOrDefaultAsync(rankSpec);
    if (rank == null)
    {
      return BadRequest("Rank not found! contact developer");
    }

    var bird = new Bird(request.Name, request.Image, 0);
    bird.SetRank(rank);
    bird.SetBirdType(birdType);

    var birdOwnerSpec = new BirdOwnerWithBirdsById(_currentUserService.TryParseBirdOwnerId());
    var birdOwner = await _birdOwnerRepository.FirstOrDefaultAsync(birdOwnerSpec);
    birdOwner!.AddBird(bird);

    await _birdOwnerRepository.SaveChangesAsync();

    var response = BirdRecord.FromEntity(bird);
    return response;
  }
}
