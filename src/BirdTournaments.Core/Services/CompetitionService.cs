using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.Core.BirdOwnerAggregate;
using BirdTournaments.Core.CompetitionAggregate.Specifications;
using BirdTournaments.Core.ContributorAggregate.Events;
using BirdTournaments.Core.ContributorAggregate;
using BirdTournaments.Core.Interfaces;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.Core.ProjectAggregate;
using BirdTournaments.SharedKernel.Interfaces;
using MediatR;
using BirdTournaments.Core.CompetitionAggregate.Events;

namespace BirdTournaments.Core.Services;
public class CompetitionService : ICompetitionService
{
  private readonly IRepository<Competition> _competitionRepository;
  private readonly IRepository<Moderator> _moderRepository;
  private readonly IRepository<Place> _placeRepository;
  private readonly IRepository<BirdType> _birdTypeRepository;
  private readonly IRepository<Bird> _birdRepository;
  private readonly IRepository<BirdOwner> _birdOwnerRepository;

  public CompetitionService(
    IRepository<Competition> repository, 
    IRepository<Moderator> moderRepository,
    IRepository<Place> placeRepository,
    IRepository<BirdType> birdTypeRepository,
    IRepository<Bird> birdRepository,
    IRepository<BirdOwner> birdOwnerRepository)
  {
    _competitionRepository = repository;
    _moderRepository = moderRepository;
    
    _placeRepository = placeRepository;
    _birdTypeRepository = birdTypeRepository;
    _birdRepository = birdRepository;

    _birdOwnerRepository = birdOwnerRepository;
  }

  private void verifyBirdBelongToOwner(Bird bird, BirdOwner birdOwner)
  {
    var isBirdBelongToOwner = birdOwner.Birds.Where(b => b == bird).Count() > 0 ? true : false;

    if (!isBirdBelongToOwner)
    {
      throw new Exception("This is not your bird");
    }
  }

  public async Task<Competition> AddNewCompetition(
    int placeId, 
    int birdTypeId, 
    DateTime date, 
    int creatorBirdId, 
    int creatorId)
  {
    var competition = new Competition(date);

    var moderatorSpec = new ModeratorByNameSpec("match_checker");
    var mod = await _moderRepository.FirstOrDefaultAsync(moderatorSpec);
    var place = await _placeRepository.GetByIdAsync(placeId);
    var birdType = await _birdTypeRepository.GetByIdAsync(birdTypeId);
    var bird = await _birdRepository.GetByIdAsync(creatorBirdId);
    var birdOwner = await _birdOwnerRepository.GetByIdAsync(creatorId);

    

    Guard.Against.Null(mod, nameof(mod));
    Guard.Against.Null(place, nameof(place));
    Guard.Against.Null(birdType, nameof(birdType));
    Guard.Against.Null(bird, nameof(bird));
    Guard.Against.Null(birdOwner, nameof(birdOwner));

    verifyBirdBelongToOwner(bird, birdOwner);

    competition.SetStatus(CompetitionStatus.WaitingForOpponent);
    competition.SetModerator(mod!);
    competition.SetPlace(place);
    competition.SetBirdType(birdType);
    competition.SetDate(date);

    var newParticipant = new Participant();
    newParticipant.SetParticipantStatus(ParticipantStatus.Joined);
    newParticipant.SetBird(bird);
    newParticipant.SetBirdOwner(birdOwner);

    competition.AddParticipant(newParticipant);

    competition = await _competitionRepository.AddAsync(competition);


    return Result<Competition>.Success(competition);
  }

  public async Task<Result> AddOpponent(int competitionId, int birdId, int ownerId)
  {
    var competition = await _competitionRepository.GetByIdAsync(competitionId);
    var bird = await _birdRepository.GetByIdAsync(birdId);
    var birdOwner = await _birdOwnerRepository.GetByIdAsync(ownerId);

    Guard.Against.Null(competition, nameof(competition));
    Guard.Against.Null(bird, nameof(bird));
    Guard.Against.Null(birdOwner, nameof(birdOwner));

    if (competition.Participants.Count == 2 || competition.Status == CompetitionStatus.Happening)
    {
      throw new Exception("Competition is happening or full slot");
    }

    verifyBirdBelongToOwner(bird, birdOwner);

    var participant = new Participant();
    participant.SetParticipantStatus(ParticipantStatus.Joined);
    participant.SetBird(bird);
    participant.SetBirdOwner(birdOwner);
    participant.SetSubmitUrl("");

    competition.AddParticipant(participant);
    competition.SetStatus(CompetitionStatus.Happening);

    await _competitionRepository.SaveChangesAsync();

    

    return Result.Success();

  }

  public async Task<ICollection<Competition>> GetWaitingCompetitionByRank(Rank rank)
  {

    var competitionSpecs = new CompetitionByRank(rank);
    var listCompetitions = await _competitionRepository.ListAsync(competitionSpecs);

    return listCompetitions;
  }
}
