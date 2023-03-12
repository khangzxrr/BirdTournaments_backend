using Ardalis.ApiEndpoints;
using BirdTournaments.Core.BirdOwnerAggregate;
using BirdTournaments.Core.BirdOwnerAggregate.Specifications;
using BirdTournaments.Core.UserAggregate;
using BirdTournaments.Core.UserAggregate.Specifications;
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


  private readonly IRepository<User> _userRepository; 


  private readonly ICurrentUserService _currentUserService;

  public Me(ICurrentUserService currentUserService, IRepository<User> userRepository) 
  {
    _currentUserService = currentUserService;
    _userRepository = userRepository;
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
    try
    {
      var spec = new UserWithBirdOwnerAndBirdsByIdSpec(_currentUserService.TryParseUserId());
      var user = await _userRepository.FirstOrDefaultAsync(spec, cancellationToken);

      var userRecord = BaseUserRecord.FromEntity(user!);

      var reponse = new MeResponse(userRecord);

      return reponse;
    }
    catch(Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
}
