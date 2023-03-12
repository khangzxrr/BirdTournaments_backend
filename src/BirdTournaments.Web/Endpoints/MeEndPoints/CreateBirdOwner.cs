using Ardalis.ApiEndpoints;
using BirdTournaments.Core.BirdOwnerAggregate;
using BirdTournaments.Core.UserAggregate;
using BirdTournaments.SharedKernel.Interfaces;
using BirdTournaments.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public class CreateBirdOwner : EndpointBaseAsync
  .WithRequest<CreateBirdOwnerRequest>
  .WithActionResult<BirdOwnerRecord>
{

  private readonly IRepository<User> _UserRepository;
  private readonly IRepository<BirdOwner> _repository;
  private readonly ICurrentUserService _currentUserService;

  public CreateBirdOwner(IRepository<User> userRepository, IRepository<BirdOwner> repository, ICurrentUserService currentUserService)
  {
    _UserRepository = userRepository;
    _repository = repository;
    _currentUserService = currentUserService;
  }



  [HttpPost(CreateBirdOwnerRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Create bird owner info",
    Description = "Create bird owner info",
    OperationId = "BirdOwner.Create",
    Tags = new[] { "BirdOwner" }
    )
   ]
  public override async Task<ActionResult<BirdOwnerRecord>> HandleAsync(CreateBirdOwnerRequest request, CancellationToken cancellationToken = default)
  {
    

    try
    {
      if (!_currentUserService.BirdOwnerId.IsNullOrEmpty())
      {
        return BadRequest("bird owner exist");
      }

      var user = await _UserRepository.GetByIdAsync(_currentUserService.TryParseUserId());
      var birdOwner = new BirdOwner(request.name, request.address, request.phoneNumber);
      birdOwner.SetUser(user!);

      await _repository.AddAsync(birdOwner);
      await _repository.SaveChangesAsync(cancellationToken);

      var response = BirdOwnerRecord.FromEntity(birdOwner);
      return Ok(response);

    }
    catch(Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
    
    
  }
}
