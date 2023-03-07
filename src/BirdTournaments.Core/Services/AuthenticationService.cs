using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ardalis.GuardClauses;
using Ardalis.Result;
using BirdTournaments.Core.Interfaces;
using BirdTournaments.Core.UserAggregate;
using BirdTournaments.Core.UserAggregate.Specifications;
using BirdTournaments.SharedKernel.Interfaces;

namespace BirdTournaments.Core.Services;
internal class AuthenticationService : IAuthenticationService
{

  private readonly IRepository<User> _userRepository;

  public AuthenticationService(IRepository<User> userRepository)
  {
    _userRepository = userRepository;
  }



  public async Task<Result<User>> AuthenticationAsync(string username, string password)
  {
    Guard.Against.NullOrEmpty(username, nameof(username));  
    Guard.Against.NullOrEmpty(password, nameof(password));

    var hashPassword = "";

    using (MD5 hash = MD5.Create())
    {
      hashPassword = String.Join
      (
          "",
          from ba in hash.ComputeHash
          (
              Encoding.UTF8.GetBytes(password)
          )
          select ba.ToString("x2")
      );
    }

    var userSpec = new UserByUsernamePassword(username, hashPassword!);
    var user = await _userRepository.FirstOrDefaultAsync(userSpec);

    Guard.Against.Null(user, nameof(user));

    return Result.Success(user);
  }
}
