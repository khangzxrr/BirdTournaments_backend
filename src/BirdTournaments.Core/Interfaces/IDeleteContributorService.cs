using Ardalis.Result;

namespace BirdTournaments.Core.Interfaces;

public interface IDeleteContributorService
{
    public Task<Result> DeleteContributor(int contributorId);
}
