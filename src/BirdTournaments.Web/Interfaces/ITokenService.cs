using BirdTournaments.Core.UserAggregate;

namespace BirdTournaments.Web.Interfaces;

public interface ITokenService
{
  public string GenerateToken(User user);
}
