using Ardalis.ApiEndpoints;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.PlaceEndpoints;

public class List : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<ListAllPlaceResponse>
{

  private IRepository<Place> _repository;

  public List(IRepository<Place> repository)
  {
    _repository = repository;
  }

  [HttpGet("/Places")]
  [SwaggerOperation(
    Summary = "Gets a list of all places",
    Description = "Gets a list of all places",
    OperationId = "Place.List",
    Tags = new[] { "PlaceEndpoints" }
    )]
  public override async Task<ActionResult<ListAllPlaceResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {
    var places = await _repository.ListAsync();

    if (places == null)
    {
      return NotFound();
    }

    
    var placeRecords = places.Select(p => new PlaceRecord(p.Id, p.Address));
    var response = new ListAllPlaceResponse(placeRecords);

    return Ok(response);
  }
}
