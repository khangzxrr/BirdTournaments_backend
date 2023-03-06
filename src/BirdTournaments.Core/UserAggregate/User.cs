
using Ardalis.GuardClauses;
using BirdTournaments.SharedKernel;
using BirdTournaments.SharedKernel.Interfaces;

namespace BirdTournaments.Core.UserAggregate;

public class User : EntityBase, IAggregateRoot
{
  public string UserName { get; private set; }
  public string Email { get; private set; }
  public string Hash { get; private set;}
  public string Salt{ get; private set; }

  public UserVerify Verify { get; }

  public UserRole Role { get; }

  public User(
    string userName,
    string email,
    string hash,
    string salt,
    UserVerify verify,
    UserRole role
    )
  {
    UserName = Guard.Against.NullOrEmpty(userName, nameof(userName));
    Email = Guard.Against.NullOrEmpty(email, nameof(email));
    Hash = Guard.Against.NullOrEmpty(hash, nameof(hash));
    Salt = Guard.Against.NullOrEmpty(salt, nameof(salt));
    

    Verify = verify;
    Role = role;
  }
}

