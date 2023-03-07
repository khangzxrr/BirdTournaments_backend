using Ardalis.ApiEndpoints;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.BirdTypeEndpoints;

public class List : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<ListAllBirdResponse>
{

  private readonly IRepository<BirdType> _repository;

  public List(IRepository<BirdType> repository)
  {
    _repository = repository;
  }

  [HttpGet("/BirdTypes")]
  [SwaggerOperation(
    Summary = "Gets a list of all bird types",
    Description = "Gets a list of all bird types",
    OperationId = "BirdType.List",
    Tags = new[] { "BirdTypeEndpoints" }
    )]
  public override async Task<ActionResult<ListAllBirdResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {
    var birdTypes = await _repository.ListAsync();

    if (birdTypes == null)
    {
      return NotFound();
    }

    var birdTypeRecords = birdTypes.Select(b => new BirdTypeRecord(b.Id, b.Name));
    var response = new ListAllBirdResponse(birdTypeRecords);

    return Ok(response);  
  }
}
