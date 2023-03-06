
using Ardalis.GuardClauses;
using OrderService.Core.UserAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace BirdTournaments.Core.UserAggregate;

public class User : EntityBase, IAggregateRoot
{
  public string UserName { get; private set; }
  public string Role { get; private set; }
  public string Email { get; private set; }
  public string Hash { get; private set;}
  public string Salt{ get; private set; }

  public bool Verify { get; private set; }
  public User(
    string userName,
    string email,
    string hash,
    string salt,
    string role,
    bool verify
    )
  {
    this.UserName = Guard.Against.NullOrEmpty(userName, nameof(userName));
    this.Email = Guard.Against.NullOrEmpty(email, nameof(email));
    this.Hash = Guard.Against.NullOrEmpty(hash, nameof(hash));
    this.Salt = Guard.Against.NullOrEmpty(salt, nameof(salt));
    this.Role = Guard.Against.NullOrEmpty(role, nameof(role));

    this.Verify = verify;
  }
}

