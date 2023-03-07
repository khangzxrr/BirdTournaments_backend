using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.Core.ParticipantAggregate;

namespace BirdTournaments.Core.Interfaces;
public interface ICompetitionService
{
  public Task<Competition> AddNewCompetition(int placeId, int birdTypeId, DateTime date, int creatorBirdId, int creatorId);
  public Task<Result> AddOpponent(Competition competition);
  public Task<ICollection<Competition>> GetWaitingCompetitionByRank(Rank rank); 
}
