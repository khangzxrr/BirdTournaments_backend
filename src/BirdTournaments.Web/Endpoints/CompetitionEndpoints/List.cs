using Ardalis.ApiEndpoints;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.Core.BirdAggregate.Specifications;
using BirdTournaments.Core.CompetitionAggregate.Specifications;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public class List : EndpointBaseAsync
  .WithRequest<GetCompetitionsByRankRequest>
  .WithActionResult<GetCompetitionsByRankResponse>
{
  private readonly IReadRepository<Competition> _repository;
  private readonly IReadRepository<Rank> rankRepository;

  public List(IReadRepository<Competition> repository, IReadRepository<Rank> rankRepository)
  {
    _repository = repository;
    this.rankRepository = rankRepository;
  }

  [HttpGet(GetCompetitionsByRankRequest.Route)]
  [SwaggerOperation(
    Summary = "Gets a list of all competitions by rank name",
    Description = "Gets a list of all competitions by rank name",
    OperationId = "Competition.List",
    Tags = new[] { "CompetitionEndpoints" }
    )]
  public override async Task<ActionResult<GetCompetitionsByRankResponse>> HandleAsync(
    [FromRoute] GetCompetitionsByRankRequest request, CancellationToken cancellationToken = default)
  {

    var rankSpec = new RankByName(request.rankName!);
    var rank = await rankRepository.FirstOrDefaultAsync(rankSpec);

    if (rank == null)
    {
      return NotFound("rank name is not found");
    }

    var competitionSpec = new CompetitionByRank(rank);
    var competitions = await _repository.ListAsync(competitionSpec);

    var competitionRecords = competitions
        .Select(competition => new CompetitionRecord(
          competition.Id, 
          competition.Date, 
          competition.Place.Address, 
          competition.BirdType.Name, 
          competition.Participants.First().Bird.Elo, 
          competition.Status.Name, 
          competition.Participants.First().BirdOwner.Name
          ))
        .ToList();
    var response = new GetCompetitionsByRankResponse(competitionRecords!);


    return Ok(response);
  }
}
