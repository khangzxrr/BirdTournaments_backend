using Ardalis.GuardClauses;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.Core.UserAggregate;
using BirdTournaments.SharedKernel;
using BirdTournaments.SharedKernel.Interfaces;

namespace BirdTournaments.Core.BirdOwnerAggregate;
public class BirdOwner : EntityBase, IAggregateRoot
{
  public string Name { get; }
  public string Address { get; }
  public string PhoneNumber { get; }
  public string VipAccountId { get; }

  public int UserId;

  public User User { get; }


  // DDD Patterns comment
  // Using a private collection field, better for DDD Aggregate's encapsulation
  // so OrderItems cannot be added from "outside the AggregateRoot" directly to the collection,
  // but only through the method Order.AddOrderItem() which includes behavior.
  private readonly List<Bird> _birds = new List<Bird>();
  // Using List<>.AsReadOnly() 
  // This will create a read only wrapper around the private list so is protected against "external updates".
  // It's much cheaper than .ToList() because it will not have to copy all items in a new collection. (Just one heap alloc for the wrapper instance)
  //https://msdn.microsoft.com/en-us/library/e78dcd75(v=vs.110).aspx 
  public IReadOnlyCollection<Bird> Birds => _birds.AsReadOnly();

  #pragma warning disable CS8618 // Required by Entity Framework
  private BirdOwner() { }
  public BirdOwner(
    string name, 
    string address, 
    string phoneNumber,
    string vipAccountId, 
    int userId, 
    User user,
    List<Bird> birds) { 
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Address = Guard.Against.NullOrEmpty(address, nameof(address));
    PhoneNumber = Guard.Against.NullOrEmpty(phoneNumber, nameof(phoneNumber));

    VipAccountId = vipAccountId;
    UserId = userId;

    User = user;
    _birds = birds;
  }

}
