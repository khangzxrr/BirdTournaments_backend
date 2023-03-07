namespace BirdTournaments.Web.Interfaces;

public interface ICurrentUserService
{
  string? UserName { get; }
  string? BirdOwnerId { get; }

  public int TryParseBirdOwnerId();
}
