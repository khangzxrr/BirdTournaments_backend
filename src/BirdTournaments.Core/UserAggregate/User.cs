
using Ardalis.GuardClauses;
using BirdTournaments.SharedKernel;
using BirdTournaments.SharedKernel.Interfaces;

namespace BirdTournaments.Core.UserAggregate;

public class User : EntityBase, IAggregateRoot
{
  public string UserName { get; private set; }
  public string Role { get; private set; }
  public string Email { get; private set; }
  public string Hash { get; private set;}
  public string Salt{ get; private set; }

  public UserVerify Verify { get; }

  public User(
    string userName,
    string email,
    string hash,
    string salt,
    string role,
    UserVerify verify
    )
  {
    UserName = Guard.Against.NullOrEmpty(userName, nameof(userName));
    Email = Guard.Against.NullOrEmpty(email, nameof(email));
    Hash = Guard.Against.NullOrEmpty(hash, nameof(hash));
    Salt = Guard.Against.NullOrEmpty(salt, nameof(salt));
    Role = Guard.Against.NullOrEmpty(role, nameof(role));

    Verify = verify;
  }
}

