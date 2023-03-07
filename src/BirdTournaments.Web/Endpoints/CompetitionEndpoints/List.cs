using Ardalis.ApiEndpoints;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.Core.CompetitionAggregate.Specifications;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class List : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<CompetitionListResponse>
{
  private readonly IReadRepository<Competition> _repository;
  private readonly IReadRepository<Rank> rankRepository;

  public List(IReadRepository<Competition> repository, IReadRepository<Rank> rankRepository)
  {
    _repository = repository;
    this.rankRepository = rankRepository;
  }

  [HttpGet("/Competitions")]
  [SwaggerOperation(
    Summary = "Gets a list of all competitions",
    Description = "Gets a list of all competitions",
    OperationId = "Competition.List",
    Tags = new[] { "CompetitionEndpoints" }
    )]
  public override async Task<ActionResult<CompetitionListResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {


    var competitionSpec = new CompetitionByRank(null);
    var competitions = await _repository.ListAsync(competitionSpec);
    var response = new CompetitionListResponse
    {
      Competitions = competitions
        .Select(competition =>  new CompetitionRecord(competition.Id, competition.Date, competition.Place.Address, competition.BirdType.Name, competition.Participants.First().Bird.Elo))
        .ToList()
    };

    return Ok(response);
  }
}
