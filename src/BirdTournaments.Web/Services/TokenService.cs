using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BirdTournaments.Core.UserAggregate;
using BirdTournaments.Web.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BirdTournaments.Web.Services;

public class TokenService : ITokenService
{
  private readonly IConfiguration _configuration;
  
  public TokenService(IConfiguration configuration)
  {
    _configuration = configuration;
  }


  public string GenerateToken(User user)
  {
    string key = _configuration["Jwt:Key"]!;
    string issuer = _configuration["Jwt:Issuer"]!;
    string audience = _configuration["Jwt:Audience"]!;

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
      new Claim(ClaimTypes.NameIdentifier,user.UserName),
      new Claim(ClaimTypes.Role, user.Role.Name),
      new Claim("birdOwnerId", (user.BirdOwner == null) ? "" : user.BirdOwner.Id.ToString())
    };

    var token = new JwtSecurityToken(
     issuer,
     audience,
     claims,
     expires: DateTime.Now.AddMinutes(90),
     signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
