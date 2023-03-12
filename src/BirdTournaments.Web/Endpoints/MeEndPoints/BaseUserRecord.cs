using BirdTournaments.Core.BirdOwnerAggregate;
using BirdTournaments.Core.UserAggregate;

namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public record BaseUserRecord(int id, string username, string email, string role, BirdOwnerRecord? birdOwnerRecord)
{
  public static BaseUserRecord FromEntity(User user)
  {

    if (user.BirdOwner == null)
    {
      return new BaseUserRecord(user.Id, user.UserName, user.Email, user.Role.Name, null);
    }

    var birdOwnerRecord = BirdOwnerRecord.FromEntity(user.BirdOwner);
    return new BaseUserRecord(user.Id, user.UserName, user.Email, user.Role.Name, birdOwnerRecord);

  }
}
