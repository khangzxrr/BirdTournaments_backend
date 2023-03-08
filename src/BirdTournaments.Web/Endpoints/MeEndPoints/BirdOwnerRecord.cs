using BirdTournaments.Core.BirdOwnerAggregate;

namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public record BirdOwnerRecord(int id, string name, string address, string phoneNumber)
{
  public static BirdOwnerRecord FromEntity(BirdOwner birdOwner)
  {
    return new BirdOwnerRecord(birdOwner.Id, birdOwner.Name, birdOwner.Address, birdOwner.PhoneNumber);
  }
}
