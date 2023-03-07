using System.Security.Claims;
using BirdTournaments.Web.Interfaces;

namespace BirdTournaments.Web.Services;

public class CurrentUserService : ICurrentUserService
{
  private readonly IHttpContextAccessor _contextAccessor;

  public CurrentUserService(IHttpContextAccessor httpContextAccessor)
  {
    _contextAccessor = httpContextAccessor;
  }


  public string? UserName => _contextAccessor.HttpContext!.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
  public string? BirdOwnerId => _contextAccessor.HttpContext!.User.Claims.Where(c => c.Type == "birdOwnerId").FirstOrDefault()?.Value;

  public int TryParseBirdOwnerId()
  {
    return int.Parse(BirdOwnerId!);
  }
}
