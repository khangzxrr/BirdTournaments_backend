using BirdTournaments.Core.BirdOwnerAggregate;

namespace BirdTournaments.Web.Endpoints.MeEndPoints;

public record BirdOwnerRecord(int id, string name, string address, string phoneNumber, IEnumerable<BirdRecord>? birdRecords)
{
  public static BirdOwnerRecord FromEntity(BirdOwner birdOwner)
  {
    if (birdOwner.Birds != null)
    {
      var birdRecords = birdOwner.Birds.Select(BirdRecord.FromEntity);

      return new BirdOwnerRecord(birdOwner.Id, birdOwner.Name, birdOwner.Address, birdOwner.PhoneNumber, birdRecords);
    }

    return new BirdOwnerRecord(birdOwner.Id, birdOwner.Name, birdOwner.Address, birdOwner.PhoneNumber, null);
  }
}
