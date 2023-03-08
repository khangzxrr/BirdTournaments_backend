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


  private string GenerateMD5(string text)
  {
    var hashPassword = "";

    using (MD5 hash = MD5.Create())
    {
      hashPassword = String.Join
      (
          "",
          from ba in hash.ComputeHash
          (
              Encoding.UTF8.GetBytes(text)
          )
          select ba.ToString("x2")
      );
    }

    return hashPassword;
  }

  public async Task<Result<User>> AuthenticationAsync(string username, string password)
  {
    Guard.Against.NullOrEmpty(username, nameof(username));  
    Guard.Against.NullOrEmpty(password, nameof(password));

    var generatedHashPassword = GenerateMD5(password);

    var userSpec = new UserByUsernamePassword(username, generatedHashPassword);
    var user = await _userRepository.FirstOrDefaultAsync(userSpec);

    Guard.Against.Null(user, nameof(user));

    return Result.Success(user);
  }

  public async Task<Result<User>> CreateNewUserAsync(string username, string password, string email)
  {
    Guard.Against.NullOrEmpty(username, nameof(username));
    Guard.Against.NullOrEmpty(password, nameof(password));
    Guard.Against.NullOrEmpty(email, nameof(email));


    var userByUsernameSpec = new UserByUsernameSpec(username);
    var user = await _userRepository.FirstOrDefaultAsync(userByUsernameSpec);

    if (user != null)
    {
      return Result.Error("username is exist");
    }

    var userByEmailSpec = new UserByEmailSpec(email);
    user = await _userRepository.FirstOrDefaultAsync(userByEmailSpec);

    if (user != null)
    {
      return Result.Error("email is exist");
    }

    user = new User(username, password, GenerateMD5(password), "salt", UserVerify.actived, UserRole.player);

    await _userRepository.AddAsync(user);
    await _userRepository.SaveChangesAsync();
    
    return Result.Success(user);
  }
}
