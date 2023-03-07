using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.Core.CompetitionAggregate.Specifications;
using BirdTournaments.Core.Interfaces;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.Core.ProjectAggregate;
using BirdTournaments.SharedKernel.Interfaces;
using MediatR;

namespace BirdTournaments.Core.Services;
public class CompetitionService : ICompetitionService
{
  private readonly IRepository<Competition> _repository;
  private readonly IRepository<Moderator> _moderRepository;
  private readonly IMediator _mediator;

  public CompetitionService(
    IRepository<Competition> repository, 
    IRepository<Moderator> moderRepository,
    IMediator mediator)
  {
    _repository = repository;
    _moderRepository = moderRepository;
    _mediator = mediator;
  }

  public async Task<Competition> AddNewCompetition(Place place, BirdType birdType)
  {
    var competition = new Competition(DateTime.Now);

    var moderatorSpec = new ModeratorByNameSpec("match_checker");
    var mod = await _moderRepository.FirstOrDefaultAsync(moderatorSpec);

    if (mod == null)
    {
      return Result<Competition>.Error("moderator is not found! [match_checker]");
    }

    competition.SetStatus(CompetitionStatus.WaitingForOpponent);
    competition.SetModerator(mod!);
    competition.SetPlace(place);
    competition.SetBirdType(birdType);
    
    competition = await _repository.AddAsync(competition);

    return Result<Competition>.Success(competition);
  }

  public Task<Result> AddOpponent(Competition competition)
  {
    throw new NotImplementedException();
  }

  public async Task<ICollection<Competition>> GetWaitingCompetitionByRank(Rank rank)
  {

    var competitionSpecs = new CompetitionByRank(rank);
    var listCompetitions = await _repository.ListAsync(competitionSpecs);

    return listCompetitions;
  }
}
