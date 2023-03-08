using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using BirdTournaments.Core.UserAggregate;

namespace BirdTournaments.Core.Interfaces;
public interface IAuthenticationService
{
  public Task<Result<User>> AuthenticationAsync(string username, string password);
  public Task<Result<User>> CreateNewUserAsync(string username, string password, string email);
}
