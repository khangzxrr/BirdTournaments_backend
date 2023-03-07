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
using BirdTournaments.Core.Interfaces;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.SharedKernel.Interfaces;

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

  public async Task<Result> SubmitCompetitionResult(int competitionId, int ownerId, bool isWin)
  {
    var competition = await _competitionRepository.GetByIdAsync(competitionId);
    var birdOwner = await _birdOwnerRepository.GetByIdAsync(ownerId);

    Guard.Against.Null(competition, nameof(competition));
    Guard.Against.Null(birdOwner, nameof(birdOwner));

    if (competition.Status != CompetitionStatus.Happening)
    {
      throw new Exception("This competition state cannot be submitted");
    }

    var isCompetitionBelongToUser = competition.Participants.Where(p => p.BirdOwner.Id == ownerId).FirstOrDefault() != null ? true : false;
    if (!isCompetitionBelongToUser)
    {
      throw new Exception("This competition is not your");
    }

    var isUserSubmitted = competition.Participants
      .Where(p => p.BirdOwner.Id == ownerId &&
              p.Status != ParticipantStatus.Joined).FirstOrDefault() != null ? true : false;
    if (isUserSubmitted)
    {
      throw new Exception("You have already submitted result");
    }

    var participant = competition.Participants.Where(p => p.BirdOwner.Id == ownerId).FirstOrDefault();
    Guard.Against.Null(participant, nameof(participant));

    participant.SetParticipantStatus(isWin ? ParticipantStatus.Win : ParticipantStatus.Lose);

    await _competitionRepository.SaveChangesAsync();

    await PerformCheckingResult(competition);

    return Result.Success();
  }

  public async Task<Result> PerformCheckingResult(Competition competition)
  {
    //check both player has submited result
    int countWin = 0;
    int countLose = 0;
    bool someOneNotSubmited = false;
    var participants = competition.Participants.ToList();
    foreach(var participant in participants)
    {
      if (participant.Status == ParticipantStatus.Lose)
      {
        countLose++;
      }
      else if (participant.Status == ParticipantStatus.Win)
      {
        countWin++;
      }
      else
      {
        someOneNotSubmited = true;
        break;
      }
    }
    

    //if someone is not submited return, without checking anything
    if (someOneNotSubmited)
    {
      return Result.Success();
    }


    if (countWin == countLose) //valid, 1 lose 1 win
    {
      competition.SetStatus(CompetitionStatus.Ended);
      //gant elo
    }
    else //someone is cheated
    {
      competition.SetStatus(CompetitionStatus.WaitingForVerify);
    }

    await _competitionRepository.SaveChangesAsync();

    return Result.Success();
  }
}
